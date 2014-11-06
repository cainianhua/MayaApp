using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web
{
    public class MvcPanel : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public MvcPanel(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                PanelExtensions.EndPanel(_viewContext);
            }
        }

        public void EndForm()
        {
            Dispose(true);
        }
    }
}