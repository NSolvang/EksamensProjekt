using System;
using System.Collections.Generic;

namespace Core.Factory
{
    public class StudentplanFactory
    {
        public static Studentplan CreateDefaultStudentplan()
        {
            return new Studentplan
            {
                StudentplanID = 1, 
                Name = "Standard elevplan",
                Description = "Denne plan gives til alle nye elever",
                Goal = new List<Goal>
                {
                    new Goal
                    {
                        GoalId = 1,
                        Name = "Inden Første Dag",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 1,
                                Name = "Bestil Uniform",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig",
                                Deadline = DateTime.Today.AddDays(3).ToShortDateString(),
                                Status = "Mangler",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 2,
                        Name = "Velkommen til & Introduktion Til Kollegaer",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 2,
                                Name = "Udlever tøj og sikkerhedssko",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = DateTime.Today.AddDays(7).ToShortDateString(),
                                Status = "Mangler",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 3,
                                Name = "Hvor er omklædning, personalekantine, toiletter?",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = DateTime.Today.AddDays(7).ToShortDateString(),
                                Status = "Mangler",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 3,
                        Name = "Information - Generel Fra Din Nærmeste Leder",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 4,
                        Name = "Sikkerhed & Arbejdsmiljø",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 5,
                        Name = "Samtaler Undervejs i Min Første Praktikperiode",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 6,
                        Name = "Kurser - Interne mv",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 7,
                        Name = "Madspild & Affaldssortering",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 8,
                        Name = "Faglige mål",
                        Subgoals = new List<Subgoal>()
                    },
                    new Goal
                    {
                        GoalId = 9,
                        Name = "Evaluering",
                        Subgoals = new List<Subgoal>()
                    }
                }
            };
        }
    }
}
