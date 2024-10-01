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
    internal class ListDjmd
    {
        /// <summary>
        /// Iterates though all people in the AddressBook and prints info about them.
        /// </summary>
        private static void Print(DjmdFile djmdFile)
        {
            foreach (GpsDataStruct gps in djmdFile.Gps)
            {
                Console.WriteLine("{0},{1},{2},{3},{4},{5}", gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates.Longitude.ToString("0.000000"),
                                            gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates.Latitude.ToString("0.000000"),
                                            gps.GpsWrapperStruct.GpsBasicStruct.GpsAltitudeMm.ToString("0"),
                                            gps.GpsWrapperStruct.VelocityStruct.X.ToString("0.00"),
                                            gps.GpsWrapperStruct.VelocityStruct.Y.ToString("0.00"),
                                            gps.GpsWrapperStruct.GpsBasicStruct.GpsTime.Time.ToString()
                                            );
                //Console.WriteLine("Long: {0}", gps.GpsWrapperStruct.GpsBasicStruct.GpsCoordinates.Longitude.ToString("0.000000"));
            }
        }

        /// <summary>
        /// Entry point - loads the addressbook and then displays it.
        /// </summary>
        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage:  ListDjmd ADDRESS_BOOK_FILE");
                return 1;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("{0} doesn't exist. Add a gps record to create the file first.", args[0]);
                return 0;
            }

            // Read the existing djmd file.
            using (Stream stream = File.OpenRead(args[0]))
            {
                DjmdFile djmdFile = DjmdFile.Parser.ParseFrom(stream);
                Print(djmdFile);
            }
            return 0;
        }
    }
}