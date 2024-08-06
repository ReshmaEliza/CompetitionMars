using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarsCompetition.ModelCert
{
    public class InputData
    {

        public certificationData certificationData { get; set; }

        public IList<certificationData> certificationDataList { get; set; }

        public EditCertificationData editcertificationData { get; set; }
        public DeleteCertificateData DeleteCertificateData { get; set; }
        public certificateDataCount certificateDataCount { get; set; }  

    }
}
