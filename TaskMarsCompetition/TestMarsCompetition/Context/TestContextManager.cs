using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarsCompetition.Context
{
    public static class TestContextManager
    {
        // Static property to hold the added education data
        public static List<string> AddedEducationData { get; set; } = new List<string>();

        public static  List<string> UpdatedEducation { get; set; } = new List<string>();

        public static void AddUpdatedEducation(string oldEducation, string newEducation)
        {
            UpdatedEducation.Add(newEducation);
        }
        public static void RemoveEdu(string edu)
        {
            AddedEducationData.Remove(edu);
            UpdatedEducation.Remove(edu);
        }

        // Static property to hold the added cert data
        public static List<string> AddedCertData { get; set; } = new List<string>();

        public static List<string> UpdatedCert { get; set; } = new List<string>();

        public static void AddUpdatedCert(string oldCert, string newCert)
        {
            UpdatedCert.Add(newCert);
        }

        public static void RemoveCert(string cert)
        {
            AddedCertData.Remove(cert);
            UpdatedCert.Remove(cert);
        }

       

    }
}
