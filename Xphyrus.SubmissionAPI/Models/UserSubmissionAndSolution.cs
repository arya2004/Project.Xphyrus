﻿using Microsoft.AspNetCore.Identity;

namespace Xphyrus.SubmissionAPI.Models
{
    public class UserSubmissionandSulition
    {   
        public int UserSubmissionandSulitionId { get; set; }
        public int LanguageCode { get; set; }

        public DateTime CreatedON { get; set; } = DateTime.UtcNow;
        public string Result { get; set; }
        public bool IsAccepted { get; set; }

        public int StudentId  { get; set; }
        public int AssignmentId { get; set; }
        public int AssesmentId { get; set; }

    }
}
