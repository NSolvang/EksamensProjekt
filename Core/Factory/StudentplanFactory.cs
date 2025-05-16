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
                                Deadline = "Inden første dag",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 2,
                                Name = "Informer om forplejning",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = "Inden første dag",
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
                                SubgoalID = 3,
                                Name = "Udlever tøj og sikkerhedssko",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = DateTime.Today.AddDays(7).ToShortDateString(),
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 4,
                                Name = "Hvor er omklædning, personalekantine, toiletter?",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = DateTime.Today.AddDays(7).ToShortDateString(),
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 3,
                        Name = "Information - generel fra din nærmeste leder",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 5,
                                Name = "Vagtplaner",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = "De første 14 dage",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 6,
                                Name = "Ferie - fridage - hvem melder jeg ind til og hvordan",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = "De første 14 dage",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 4,
                        Name = "Sikkerhed & Arbejdsmiljø",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 7,
                                Name = "Introduktion til arbejdsmiljø på Comwell Connect og AMU på hotellet",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig",
                                Deadline = "I løbet af den første måned",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 8,
                                Name = "Ergonomi - herunder tunge løft",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig",
                                Deadline = "I løbet af den første måned",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 5,
                        Name = "Samtaler Undervejs i Min Første Praktikperiode",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 9,
                                Name = "6 ugers samtale - trivsel og forventningsafstemning ",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = "Efter 6. uge",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 10,
                                Name = "3 måneders samtale - inden prøveperiodens afslutning - evaluering af de første 3 mdr.",
                                Date = DateTime.Today,
                                Responsible = "Elevansvarlig / Nærmeste leder",
                                Deadline = "Efter 6. uge",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 6,
                        Name = "Kurser - Interne mv",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 11,
                                Name = "Kernen i Comwell - intro for nye medarbejdere - Herunder Historie, koncepter, DNA, Do's&Dont's, 10\nløfter og klagehåndtering  ",
                                Date = DateTime.Today,
                                Responsible = "HR",
                                Deadline = "Tilmeldes efter prøvetid",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 12,
                                Name = "Kernen i Comwell - ESG - Bæredygtighed ",
                                Date = DateTime.Today,
                                Responsible = "HR",
                                Deadline = "Tilmeldes efter prøvetid",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 7,
                        Name = "Madspild & Affaldssortering",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 13,
                                Name = "Kendskab til Esmiley-system - herunder betjening og årshjul i madspild ",
                                Date = DateTime.Today,
                                Responsible = "Nærmeste leder/anden",
                                Deadline = "0-3 mdr.",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 14,
                                Name = "Foretage affaldssortering i køkkenet ",
                                Date = DateTime.Today,
                                Responsible = "Nærmeste leder/anden",
                                Deadline = "0-3 mdr.",
                                Comments = "",
                                Approval = false
                            }
                        }
                    },
                    new Goal
                    {
                        GoalId = 8,
                        Name = "Faglige mål",
                        Subgoals = new List<Subgoal>
                        {
                            new Subgoal
                            {
                                SubgoalID = 15,
                                Name = "Kendskab og gennemgang af systemer, som bruges i køkken",
                                Date = DateTime.Today,
                                Responsible = "Nærmeste leder/anden",
                                Deadline = "I praktikperioden",
                                Comments = "",
                                Approval = false
                            },
                            new Subgoal
                            {
                                SubgoalID = 16,
                                Name = "Gennemgang af de forskellige knives funktionalitet og brugsegenskaber\n(Eleven får 2000 kr. til at shoppe knive for)",
                                Date = DateTime.Today,
                                Responsible = "Nærmeste leder/anden",
                                Deadline = "Efter prøvetid",
                                Comments = "",
                                Approval = false
                            }
                        }
                    }
                }
            };
        }
    }
}
