using System;

namespace AppWeb.Controllers
{
    internal class RolAuthorizeAttribute : Attribute
    {
        private int v;
        private int v1;
        private int v2;

        public RolAuthorizeAttribute(int v)
        {
            this.v = v;
        }

        public RolAuthorizeAttribute(int v, int v1, int v2) : this(v)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}