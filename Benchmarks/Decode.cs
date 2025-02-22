﻿using System.IO;
using BenchmarkDotNet.Attributes;
using PizzaQoi;
using SharpQoi;

namespace Benchmarks
{
    public unsafe class Decode
    {
        public byte[][] images;

        [GlobalSetup]
        public void Setup()
        {
            images = new byte[][]
            {
                File.ReadAllBytes("0.qoi")
            };
        }

        [Benchmark]
        public void Qoi_RGB()
        {
            byte[] qoi = images[0];

            fixed (byte* qoiData = qoi)
            {
                void* raw = Qoi.Decode(qoiData, (uint)qoi.Length, out var desc, 0);
                Qoi.Free(raw);
            }
        }

        [Benchmark]
        public void Pqoi_RGB()
        {
            byte[] qoi = images[0];

            fixed (byte* qoiData = qoi)
            {
                void* raw = Pqoi.Decode(qoiData, (uint)qoi.Length, out var desc, 0);
                Pqoi.Free(raw);
            }
        }
    }
}
