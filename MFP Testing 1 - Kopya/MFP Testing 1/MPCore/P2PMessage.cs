// Decompiled with JetBrains decompiler
// Type: MultiplayerMod.P2PMessage
// Assembly: MultiplayerMod, Version=0.10.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 65D033A0-3461-48E3-8420-08AA3203BF51
// Assembly location: C:\Users\orhan\Downloads\MpMod\Mods\MultiplayerMod.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class P2PMessage
{
    private readonly List<byte[]> byteChunks = new List<byte[]>();
    public readonly byte[] rBytes;
    public int rPos;
    private const float FLOAT_PRECISION_MULT = 32767f;

    public P2PMessage()
    {
    }

    public P2PMessage(byte[] bytes)
    {
        this.rBytes = bytes;
    }

    public byte[] GetBytes()
    {
        int length = 0;
        foreach (byte[] byteChunk in this.byteChunks)
            length += byteChunk.Length;
        byte[] numArray = new byte[length];
        int index = 0;
        foreach (byte[] byteChunk in this.byteChunks)
        {
            byteChunk.CopyTo((Array)numArray, index);
            index += byteChunk.Length;
        }
        return numArray;
    }

    public void WriteByte(byte b)
    {
        this.byteChunks.Add(new byte[1] { b });
    }

    public void WriteFloat(float f)
    {
        this.byteChunks.Add(BitConverter.GetBytes(f));
    }

    public void WriteShort(short s)
    {
        this.byteChunks.Add(BitConverter.GetBytes(s));
    }

    public void WriteInteger(int i)
    {
        this.byteChunks.Add(BitConverter.GetBytes(i));
    }

    public void WriteObject(object obj)
    {
        if(obj is bool)
        {
            WriteBool((bool)obj);
            return;
        }

        if (obj is int)
        {
            WriteInteger((int)obj);
            return;
        }

        if (obj is string)
        {
            WriteUnicodeString((string)obj);
            return;
        }
        if (obj is float)
        {
            WriteFloat((float)obj);
            return;
        }
        if (obj is Vector3)
        {
            WriteVector3((Vector3)obj);
            return;
        }
        if (obj is Quaternion)
        {
            WriteCompressedQuaternion((Quaternion)obj);
            return;
        }
        if (obj is ulong)
        {
            WriteUlong((ulong)obj);
            return;
        }
        if (obj is Steamworks.CSteamID)
        {
            Steamworks.CSteamID ID = (Steamworks.CSteamID)obj;
            WriteUlong(ID.m_SteamID);
            return;
        }

    }


    public void WriteBool(bool boolean)
    {
        WriteByte(Convert.ToByte(boolean));
    }

    public void WriteVector2(Vector2 v2)
    {
        this.WriteFloat(v2.x);
        this.WriteFloat(v2.y);
    }

    public void WriteVector3(Vector3 v3)
    {
        this.WriteFloat(v3.x);
        this.WriteFloat(v3.y);
        this.WriteFloat(v3.z);
    }

    public void WriteCompressedVector3(Vector3 v3, Vector3 basis, float range = 2f)
    {
        Vector3 vector3 = (v3 - basis) * ((float)short.MaxValue / range);
        short x = (short)vector3.x;
        short y = (short)vector3.y;
        short z = (short)vector3.z;
        this.WriteShort(x);
        this.WriteShort(y);
        this.WriteShort(z);
    }

    public void WriteQuaternion(Quaternion q)
    {
        this.WriteFloat(q.x);
        this.WriteFloat(q.y);
        this.WriteFloat(q.z);
        this.WriteFloat(q.w);
    }

    public void WriteCompressedQuaternion(Quaternion rotation)
    {
        byte b = 0;
        float a = float.MinValue;
        float num1 = 1f;
        for (int idx = 0; idx < 4; ++idx)
        {
            float num2 = rotation.Idx(idx);
            float num3 = Mathf.Abs(rotation.Idx(idx));
            if ((double)num3 > (double)a)
            {
                num1 = (double)num2 < 0.0 ? -1f : 1f;
                b = (byte)idx;
                a = num3;
            }
        }
        if (Mathf.Approximately(a, 1f))
        {
            this.WriteByte((byte)((uint)b + 4U));
            this.WriteShort((short)0);
            this.WriteShort((short)0);
            this.WriteShort((short)0);
        }
        else
        {
            short s1;
            short s2;
            short s3;
            switch (b)
            {
                case 0:
                    s1 = (short)((double)rotation.y * (double)num1 * (double)short.MaxValue);
                    s2 = (short)((double)rotation.z * (double)num1 * (double)short.MaxValue);
                    s3 = (short)((double)rotation.w * (double)num1 * (double)short.MaxValue);
                    break;
                case 1:
                    s1 = (short)((double)rotation.x * (double)num1 * (double)short.MaxValue);
                    s2 = (short)((double)rotation.z * (double)num1 * (double)short.MaxValue);
                    s3 = (short)((double)rotation.w * (double)num1 * (double)short.MaxValue);
                    break;
                case 2:
                    s1 = (short)((double)rotation.x * (double)num1 * (double)short.MaxValue);
                    s2 = (short)((double)rotation.y * (double)num1 * (double)short.MaxValue);
                    s3 = (short)((double)rotation.w * (double)num1 * (double)short.MaxValue);
                    break;
                default:
                    s1 = (short)((double)rotation.x * (double)num1 * (double)short.MaxValue);
                    s2 = (short)((double)rotation.y * (double)num1 * (double)short.MaxValue);
                    s3 = (short)((double)rotation.z * (double)num1 * (double)short.MaxValue);
                    break;
            }
            this.WriteByte(b);
            this.WriteShort(s1);
            this.WriteShort(s2);
            this.WriteShort(s3);
        }
    }

    public Quaternion ReadCompressedQuaternion()
    {
        byte num1 = this.ReadByte();
        double num2;
        switch (num1)
        {
            case 4:
                num2 = 1.0;
                break;
            case 5:
            case 6:
            case 7:
                num2 = 0.0;
                break;
            default:
                float num3 = (float)this.ReadShort() / (float)short.MaxValue;
                float num4 = (float)this.ReadShort() / (float)short.MaxValue;
                float num5 = (float)this.ReadShort() / (float)short.MaxValue;
                float num6 = Mathf.Sqrt((float)(1.0 - ((double)num3 * (double)num3 + (double)num4 * (double)num4 + (double)num5 * (double)num5)));
                switch (num1)
                {
                    case 0:
                        return new Quaternion(num6, num3, num4, num5);
                    case 1:
                        return new Quaternion(num3, num6, num4, num5);
                    case 2:
                        return new Quaternion(num3, num4, num6, num5);
                    default:
                        return new Quaternion(num3, num4, num5, num6);
                }
        }
        float num7 = num1 == (byte)5 ? 1f : 0.0f;
        float num8 = num1 == (byte)6 ? 1f : 0.0f;
        float num9 = num1 == (byte)7 ? 1f : 0.0f;
        int num10 = (int)this.ReadShort();
        int num11 = (int)this.ReadShort();
        int num12 = (int)this.ReadShort();
        double num13 = (double)num7;
        double num14 = (double)num8;
        double num15 = (double)num9;
        return new Quaternion((float)num2, (float)num13, (float)num14, (float)num15);
    }

    public Vector3 ReadCompressedVector3(Vector3 basis, float range = 2f)
    {
        int num1 = (int)this.ReadShort();
        short num2 = this.ReadShort();
        short num3 = this.ReadShort();
        double num4 = (double)num1 / (double)short.MaxValue * (double)range;
        float num5 = (float)num2 / (float)short.MaxValue * range;
        float num6 = (float)num3 / (float)short.MaxValue * range;
        double num7 = (double)num5;
        double num8 = (double)num6;
        return new Vector3((float)num4, (float)num7, (float)num8) + basis;
    }

    public void WriteUnicodeString(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        this.WriteByte((byte)bytes.Length);
        this.byteChunks.Add(bytes);
    }

    public void WriteUlong(ulong l)
    {
        this.byteChunks.Add(BitConverter.GetBytes(l));
    }

    public byte ReadByte(bool readWithoutMoving = false) // super useful for reading event only
    {
        int rByte = (int)this.rBytes[this.rPos];
        if(!readWithoutMoving)
        ++this.rPos;
        return (byte)rByte;
    }

    public float ReadFloat()
    {
        double single = (double)BitConverter.ToSingle(this.rBytes, this.rPos);
        this.rPos += 4;
        return (float)single;
    }

    public short ReadShort()
    {
        int int16 = (int)BitConverter.ToInt16(this.rBytes, this.rPos);
        this.rPos += 2;
        return (short)int16;
    }

    public int ReadInteger()
    {
        int integer = (int)BitConverter.ToInt32(this.rBytes, this.rPos);
        this.rPos += 4;
        return integer;
    }

    public Vector2 ReadVector2()
    {
        return new Vector2(this.ReadFloat(), this.ReadFloat());
    }

    public Vector3 ReadVector3()
    {
        return new Vector3(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
    }

    public Quaternion ReadQuaternion()
    {
        return new Quaternion(this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
    }

    public ulong ReadUlong()
    {
        long uint64 = (long)BitConverter.ToUInt64(this.rBytes, this.rPos);
        this.rPos += 8;
        return (ulong)uint64;
    }

    public string ReadString()
    {
        byte num = this.ReadByte();
        char[] chArray = new char[(int)num];
        for (int index = 0; index < (int)num; ++index)
            chArray[index] = (char)this.ReadByte();
        return new string(chArray);
    }

    public string ReadUnicodeString()
    {
        byte num = this.ReadByte();
        string str = Encoding.UTF8.GetString(this.rBytes, this.rPos, (int)num);
        this.rPos += (int)num;
        return str;
    }
    public bool ReadBool()
    {
        return ReadByte().Equals(1);
    }

    public bool ReachedEndOfPackage()
    {
        return rPos > rBytes.Length - 1;
    }


}

public static class QuatExt
{
    public static float Idx(this Quaternion q, int idx)
    {
        switch (idx)
        {
            case 0:
                return q.x;
            case 1:
                return q.y;
            case 2:
                return q.z;
            case 3:
                return q.w;
            default:
                return 0.0f;
        }
    }
}