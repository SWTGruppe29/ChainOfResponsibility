using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class ConsolePrinter : IConsolePrinter
    {
        /// <summary>
        /// Receives a List of tracks currently in the airspace, and tracks in conflict by 
        /// </summary>
        /// <param name="tracks">Tracks currently in the airspace</param>
        /// <param name="conflictTags">Tracks in breaking separation condition in the airspace</param>
        public void Print(List<Track> tracks, List<Conflict> conflictTags) 
        {
            //Console.Clear(); //Uncomment this when not unittesting
            
            if (!PrintTracks(tracks))
                return;
            
            printConflicts(conflictTags);
        }

        public void ConsoleSeparationDataHandler(object sender, ConsoleSeparationEventArgs args)
        {
            Print(args.tracks, args.conflictList);
        }

        /// <summary>
        /// Returns false if no airplanes was in conflict, else true
        /// </summary>
        /// <param name="conflictTags"></param>
        /// <returns></returns>
        public bool printConflicts(List<Conflict> conflictTags)
        {
            if (conflictTags.Count == 0)
            {
                Console.WriteLine("No Airplanes in conflict");
                return false;
            }

            foreach (var conflict in conflictTags)
            {
                Console.WriteLine("Airplanes in conflict");

                Console.WriteLine($"Airplane tag: {conflict.Tag1}");
                Console.WriteLine($"Airplane tag: {conflict.Tag2}");
            }

            return true;
        }

        /// <summary>
        /// Returns false if no airplanes was in the airspace
        /// </summary>
        /// <param name="tracks"></param>
        /// <returns></returns>
        public bool PrintTracks(List<Track> tracks)
        {
            if (!tracks.Any())
            {
                Console.WriteLine("No airplanes currently in the airspace");
                return false;
            }

            Console.Write("Airplane Tracks currently in the airspace: ");
            Console.WriteLine(tracks.Count);

            foreach (var track in tracks)
            {

                Console.Write($"Flight tag: " + track.Tag +
                              $" X: {track.XCoordinate} " +
                              $"Y: {track.YCoordinate} " +
                              $"Altitude: {track.Altitude}");
                if (track.CurrentCompCourse != null && track.Velocity != null)
                {
                    Console.Write($" Horizontal Velocity: {track.Velocity} " +
                                  $"Compass Course: {track.CurrentCompCourse}");
                }
                Console.WriteLine();
            }

            return true;
        }
    }
}
