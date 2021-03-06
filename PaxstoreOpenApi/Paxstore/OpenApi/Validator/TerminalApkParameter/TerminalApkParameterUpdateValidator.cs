﻿using FluentValidation;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.TerminalApkParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.TerminalApkParameter
{
    class TerminalApkParameterUpdateValidator: AbstractValidator<UpdateApkParameterRequest>
    {
        public TerminalApkParameterUpdateValidator()
        {
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFilesLength).WithMessage("Exceed max counter (10) of file type parameters!");
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFileSize).WithMessage("Exceed max size (500kb) per file type parameters!");
        }

        private bool validateParameterFilesLength(List<FileParameter> base64FileParameters)
        {
            if (base64FileParameters != null)
            {
                if (base64FileParameters.Count > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private bool validateParameterFileSize(List<FileParameter> base64FileParameters)
        {
            if (base64FileParameters != null)
            {
                for (int i = 0; i < base64FileParameters.Count; i++)
                {
                    if (Base64FileUtil.GetBase64FileSizeKB(base64FileParameters[i].FileData) > 500)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
