using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to hold all the videos
            List<Video> videos = new List<Video>();

            // ----- Video 1 -----
            Video video1 = new Video("Learning C# Classes", "CodeAcademy", 600);
            video1.AddComment(new Comment("Alice", "This really helped me understand classes, thanks!"));
            video1.AddComment(new Comment("Bob", "Great explanation and examples."));
            video1.AddComment(new Comment("Charlie", "Can you make one about interfaces next?"));
            videos.Add(video1);

            // ----- Video 2 -----
            Video video2 = new Video("Top 10 Dungeons & Dragons Spells", "DungeonMasterDave", 900);
            video2.AddComment(new Comment("Eli", "Fireball will always be my favorite spell."));
            video2.AddComment(new Comment("Mara", "You forgot about Counterspell!"));
            video2.AddComment(new Comment("Tess", "Loved the editing on this video."));
            videos.Add(video2);

            // ----- Video 3 -----
            Video video3 = new Video("How to Cook Perfect Pasta", "KitchenPro", 480);
            video3.AddComment(new Comment("Sam", "I tried this and it turned out amazing!"));
            video3.AddComment(new Comment("Jordan", "What kind of salt do you recommend?"));
            video3.AddComment(new Comment("Lily", "This was so easy to follow."));
            videos.Add(video3);

            // ----- Video 4 (optional, but within the 3–4 range) -----
            Video video4 = new Video("Relaxing Lo-Fi Music for Studying", "ChillBeats", 3600);
            video4.AddComment(new Comment("Nate", "Perfect background music for homework."));
            video4.AddComment(new Comment("Jade", "I listen to this every night while coding."));
            video4.AddComment(new Comment("Rin", "Please make a 10-hour version!"));
            videos.Add(video4);

            // ----- Display all videos and their comments -----
            foreach (Video video in videos)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Title:   {video.Title}");
                Console.WriteLine($"Author:  {video.Author}");
                Console.WriteLine($"Length:  {video.LengthInSeconds} seconds");
                Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments List:");

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($" - {comment.CommenterName}: {comment.Text}");
                }

                Console.WriteLine(); // Blank line between videos
            }

            // Keep the console window open (optional depending on your setup)
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
