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
                _id = 1,
                Name = "Standard elevplan",
                Description = "Denne plan gives til alle nye elever",
                Internship = new List<Internship>
                {
                    // Internship 1 med alle dine eksisterende goals
                    new Internship
                    {
                        _id = 1,
                        InternshipName = "Praktikperiode 1",
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Informer om forplejning",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig / Nærmeste leder",
                                        Deadline = "Inden første dag",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 4,
                                        Name = "Hvor er omklædning, personalekantine, toiletter?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig / Nærmeste leder",
                                        Deadline = DateTime.Today.AddDays(7).ToShortDateString(),
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 6,
                                        Name = "Ferie - fridage - hvem melder jeg ind til og hvordan",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig / Nærmeste leder",
                                        Deadline = "De første 14 dage",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 8,
                                        Name = "Ergonomi - herunder tunge løft",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig",
                                        Deadline = "I løbet af den første måned",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 10,
                                        Name = "3 måneders samtale - inden prøveperiodens afslutning - evaluering af de første 3 mdr.",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig / Nærmeste leder",
                                        Deadline = "Efter 6. uge",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 12,
                                        Name = "Kernen i Comwell - ESG - Bæredygtighed ",
                                        Date = DateTime.Today,
                                        Responsible = "HR",
                                        Deadline = "Tilmeldes efter prøvetid",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 14,
                                        Name = "Foretage affaldssortering i køkkenet ",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "0-3 mdr.",
                                        Comments = new List<Comment>(),
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
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 16,
                                        Name = "Gennemgang af de forskellige knives funktionalitet og brugsegenskaber\n(Eleven får 2000 kr. til at shoppe knive for)",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "Efter prøvetid",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            }
                        }
                    },

                    //Internship 2
                    new Internship
                    {
                        _id = 2,
                        InternshipName = "Praktikperiode 2",
                        Goal = new List<Goal>
                        {
                            new Goal
                            {
                                GoalId = 1,
                                Name = "Evaluering",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Første praktikperiode - nåede du gennem alle målene?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 1",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Første skoleperiode og pensum - var der noget du manglede?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 1",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 3,
                                        Name = "Gennemgang af uddannelsesplanen for kommende praktikperiode",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 1",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 2,
                                Name = "Interne kurser",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Førstehjælpskursus",
                                        Date = DateTime.Today,
                                        Responsible = "Hotel",
                                        Deadline = "Planlægges",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Crosstraining til andet hotel - elevudveksling i en uge ",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste leder",
                                        Deadline = "Efter aftale",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 3,
                                Name = "Faglige mål",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Udskæring af kød",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste Leder/anden",
                                        Deadline = "I praktikperiode 2",    
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Fisk, skaldyr, bløddy og fjerkræ",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste Leder/anden",
                                        Deadline = "I praktikperiode 2",    
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            }
                        }
                    }, 
                    //Internship 3
                    new Internship
                    {
                        _id = 3,
                        InternshipName = "Praktikperiode 3",
                        Goal = new List<Goal>
                        {
                            new Goal
                            {
                                GoalId = 1,
                                Name = "Evaluering",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Anden praktikperiode - nåede du gennem alle målene?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 2",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Anden skoleperiode og pensum - var der noget du manglede?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 2",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 3,
                                        Name = "Gennemgang af uddannelsesplanen for kommende praktikperiode",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 2",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 2,
                                Name = "Interne kurser",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "F&B elev-tur (3 dage)",
                                        Date = DateTime.Today,
                                        Responsible = "Uddannelsesansvarlig/HR",
                                        Deadline = "Forår/Sensommer",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Elev-ERFA (Kun elever som er udvalgt til dette deltager) ",
                                        Date = DateTime.Today,
                                        Responsible = "HR",
                                        Deadline = "1x om året",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 3,
                                Name = "Faglige mål",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Menusammensætning",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "I praktikperiode 3",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Selvstændigt kunne køre konference",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "I praktikperiode 3",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 4,
                                Name = "Klar-parat til fagprøve",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Gennemgang af indhold til fagprøven",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "I praktikperiode 4",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 2,
                                        Name = "Evt. mangler fra praktikperide 3 og individuelle ønsker ",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "I praktikperiode 4",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    },
                                    new Subgoal
                                    {
                                        SubgoalID = 3,
                                        Name = "Karrieresamtale med køkkenchef omkring muligheder  ",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder/anden",
                                        Deadline = "I praktikperiode 4",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            }
                        }
                    }, 
                    new Internship
                    {
                        _id = 4,
                        InternshipName = "Praktikperiode 4",
                        Goal = new List<Goal>
                        {
                           new Goal
                            {
                                GoalId = 1,
                                Name = "Evaluering",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Tredje skoleperiode og pensum - var der noget, du manglede?",
                                        Date = DateTime.Today,
                                        Responsible = "Elevansvarlig/Nærmeste Leder",
                                        Deadline = "Efter skoleperiode 3",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            },
                            new Goal
                            {
                                GoalId = 2,
                                Name = "Interne kurser",
                                Subgoals = new List<Subgoal>
                                {
                                    new Subgoal
                                    {
                                        SubgoalID = 1,
                                        Name = "Karrieresamtale med nærmeste leder og evt. HR",
                                        Date = DateTime.Today,
                                        Responsible = "Nærmeste leder",
                                        Deadline = "Planlægges",
                                        Comments = new List<Comment>(),
                                        Approval = false
                                    }
                                }
                            }
                        }
                    }, 
                }
            };
        }
    }
}
