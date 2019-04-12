using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Classes;
using ATMEnhanced.Interfaces;

namespace ATMEnhanced.Classes
{
    public class ConsolePrinter : Handler, IConsolePrinter
    {
        /// <summary>
        /// Receives a List of tracks currently in the airspace, and tracks in conflict by 
        /// </summary>
        /// <param name="tracks">Tracks currently in the airspace</param>
        /// <param name="conflictTags">Tracks in breaking separation condition in the airspace</param>
        public void Print(List<Track> tracks, List<Conflict> conflictTags) 
        {
            if (!PrintTracks(tracks))
                return;
            
            PrintConflicts(conflictTags);
        }

        public override void Handle(object data)
        {
            Console.Clear();
            TrackData trackData = (TrackData) data;
            Print(trackData.Tracks, trackData.Conflicts);
            base.Handle(data);
        }


        /// <summary>
        /// Returns false if no airplanes was in conflict, else true
        /// </summary>
        /// <param name="conflictTags"></param>
        /// <returns></returns>
        public bool PrintConflicts(List<Conflict> conflictTags)
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
