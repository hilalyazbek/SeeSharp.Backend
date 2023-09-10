using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Common.Interfaces;
public interface ICurrentUser
{
    Guid GetUserId();
}
