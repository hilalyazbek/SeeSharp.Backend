using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Domain.Constants;
public abstract class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string Contributor = nameof(Contributor);
    public const string BasicUser = nameof(BasicUser);
}