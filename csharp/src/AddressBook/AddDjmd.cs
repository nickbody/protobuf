#region Copyright notice and license
// Protocol Buffers - Google's data interchange format
// Copyright 2008 Google Inc.  All rights reserved.
//
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file or at
// https://developers.google.com/open-source/licenses/bsd
#endregion

using System;
using System.IO;

namespace Google.Protobuf.Examples.Sprayer
{
    internal class AddDjmd
    {
        /// <summary>
        /// Builds a GpsDataStruct record
        /// </summary>
        private static GpsDataStruct CreateGpsDataStruct(TextReader input, TextWriter output)
        {
            output.WriteLine("Adding one gps record");

            GpsDataStruct gps = new GpsDataStruct();
            gps.GpsTimestampStruct = new GpsTimestampStruct();
            gps.ImuStruct = new ImuStruct();
            gps.GpsWrapperStruct = new GpsWrapperStruct();

            gps.GpsTimestampStruct.Frameindex = 2;
            gps.GpsTimestampStruct.FrameTimestampUs = 2206393266;

            gps.ImuStruct.ImuVal1Struct = new ImuVal1Struct();
            gps.ImuStruct.ImuVal1Struct.Value1 = 100.0F;

            gps.ImuStruct.ImuVal2Struct = new ImuVal2Struct();
            gps.ImuStruct.ImuVal2Struct.Value1 = 630.0F;

            gps.ImuStruct.ImuChunk1Struct = new ImuChunk1Struct();
            byte[] temp = new byte[] { 1, 0xb0, 6 };
            gps.ImuStruct.ImuChunk1Struct.Value1 = ByteString.CopyFrom(temp);
            gps.ImuStruct.ImuVal3Struct = new ImuVal3Struct();
            gps.ImuStruct.ImuVal3Struct.Value1 = 1.0F;

            gps.ImuStruct.ImuVal4Struct = new ImuVal4Struct();
            gps.ImuStruct.ImuVal4Struct.Value1 = 5500;

            gps.ImuStruct.ImuEmpty7Struct = new ImuEmptyStruct();
            gps.ImuStruct.ImuEmpty8Struct = new ImuEmptyStruct();

            gps.ImuStruct.ImuQuaternionStruct = new ImuQuaternionStruct();
            gps.ImuStruct.ImuQuaternionStruct.W = 0.6543F;
            gps.ImuStruct.ImuQuaternionStruct.X = -0.0345F;
            gps.ImuStruct.ImuQuaternionStruct.Y = -0.0456F;
            gps.ImuStruct.ImuQuaternionStruct.Z = -0.789F;

            gps.ImuStruct.ImuGravityStruct = new ImuGravityStruct();
            gps.ImuStruct.ImuGravityStruct.X = 0.123F;
            gps.ImuStruct.ImuGravityStruct.Y = 0.456F;
            gps.ImuStruct.ImuGravityStruct.Z = -0.777F;

            gps.ImuStruct.ImuEmpty11Struct = new ImuEmptyStruct();
            gps.ImuStruct.ImuEmpty12Struct = new ImuEmptyStruct();

            gps.GpsWrapperStruct.GpsWrapperHeaderStruct = new GpsWrapperHeaderStruct();
            gps.GpsWrapperStruct.GpsBasicStruct = new GpsBasic();
            gps.GpsWrapperStruct.VelocityStruct = new VelocityStruct();

            gps.GpsWrapperStruct.GpsWrapperHeaderStruct.Value1 = 1;
            gps.GpsWrapperStruct.GpsWrapperHeaderStruct.Value2 = 1;
            gps.GpsWrapperStruct.GpsWrapperHeaderStruct.Ac003 = "DJI AC003";
            gps.GpsWrapperStruct.GpsWrapperHeaderStruct.FrameRate = 29.9700F;

            gps.GpsWrapperStruct.GpsBasicStruct.GpsStatus = GpsBasic.Types.GpsStatus.GpsNormal;
            gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates = new PositionCoord();
            gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates.Latitude = -38.1234;
            gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates.Longitude = 175.1234;

            gps.GpsWrapperStruct.GpsBasicStruct.GpsAltitudeMm = 70000;
            gps.GpsWrapperStruct.GpsBasicStruct.GpsAltitudeType = GpsBasic.Types.GpsAltType.GpsFusionAltitude;
            gps.GpsWrapperStruct.GpsBasicStruct.HasGpsTime = true;
            gps.GpsWrapperStruct.GpsBasicStruct.GpsTime = new GpsTime();
            gps.GpsWrapperStruct.GpsBasicStruct.GpsTime.Time = "2024-09-19 22:09:10";

            gps.GpsWrapperStruct.VelocityStruct.X = -3.1F;
            gps.GpsWrapperStruct.VelocityStruct.Y = 0.1F;
            gps.GpsWrapperStruct.VelocityStruct.Z = -0.1F;

            return gps;
        }

        /// <summary>
        /// Entry point - loads an existing djmd file or creates a new one,
        /// then writes it back to the file.
        /// </summary>
        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage:  AddDjmd DJMD_FILE");
                return -1;
            }

            DjmdFile djmdFile;

            if (File.Exists(args[0]))
            {
                using Stream file = File.OpenRead(args[0]);
                djmdFile = DjmdFile.Parser.ParseFrom(file);
            }
            else
            {
                Console.WriteLine("{0}: File not found. Creating a new file.", args[0]);
                djmdFile = new DjmdFile();
                djmdFile.Header = new Header();
                djmdFile.Header.HeaderText = new HeaderText();
                djmdFile.Header.HeaderSect2 = new HeaderSect2();
                djmdFile.Header.HeaderSect9 = new HeaderSect9();
                djmdFile.SecondHeader = new SecondHeader();
                djmdFile.SecondHeader.SecondHeaderName = new SecondHeaderName();
                djmdFile.SecondHeader.SecondHeaderFrame = new SecondHeaderFrame();
                djmdFile.SecondHeader.SecondHeaderEmpty = new SecondHeaderEmpty();
                djmdFile.SecondHeader.SecondHeaderValue = new SecondHeaderValue();

                djmdFile.Header.HeaderText.Proto = "dvtm_ac203.proto";
                djmdFile.Header.HeaderText.Ver1 = "02.00.04";
                djmdFile.Header.HeaderText.Ver2 = "2.0.1";
                djmdFile.Header.HeaderText.Sn = "6S6XM8J00AKDY2";
                djmdFile.Header.HeaderText.Firmware = "10.04.00.10";
                djmdFile.Header.HeaderText.TimestampUs = 2177230750;
                djmdFile.Header.HeaderText.Camera = "DJI OsmoAction4";

                djmdFile.Header.HeaderSect2.Value1 = 1;
                djmdFile.Header.HeaderSect2.Value2 = 1;

                djmdFile.Header.HeaderSect9.Value1 = 1;

                djmdFile.SecondHeader.SecondHeaderName.Track = "video";

                djmdFile.SecondHeader.SecondHeaderFrame.Width = 1920;
                djmdFile.SecondHeader.SecondHeaderFrame.Height = 1080;
                djmdFile.SecondHeader.SecondHeaderFrame.FrameRate = 29.9700F;
                djmdFile.SecondHeader.SecondHeaderFrame.Value1 = 1;
                djmdFile.SecondHeader.SecondHeaderFrame.Value2 = 8;
                djmdFile.SecondHeader.SecondHeaderFrame.Value3 = 4;
                djmdFile.SecondHeader.SecondHeaderFrame.Value4 = 1;

                djmdFile.SecondHeader.SecondHeaderValue.Value1 = 1;
            }

            // Add a record.
            djmdFile.Gps.Add(CreateGpsDataStruct(Console.In, Console.Out));

            // Write the new djmd back to disk.
            using (Stream output = File.OpenWrite(args[0]))
            {
                djmdFile.WriteTo(output);
            }
            return 0;
        }
    }
}