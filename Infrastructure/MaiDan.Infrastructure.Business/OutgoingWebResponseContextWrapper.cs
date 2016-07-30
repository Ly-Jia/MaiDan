﻿using System;
using System.ServiceModel.Web;
using MaiDan.Infrastructure.Contract;

namespace MaiDan.Infrastructure.Business
{
    /***
     * Code taken from http://pastebin.com/CjUMwhsG & http://www.whatibroke.com/2014/10/22/resources-for-mocking-wcf/
     ***/

    public class OutgoingWebResponseContextWrapper : IOutgoingWebResponseContext
    {
        private OutgoingWebResponseContext context;

        public OutgoingWebResponseContextWrapper(OutgoingWebResponseContext context)
        {
            this.context = context;
        }

        public void SetStatusAsCreated(Uri locationUri)
        {
            context.SetStatusAsCreated(locationUri);
        }

        public void SetStatusAsNotFound()
        {
            context.SetStatusAsNotFound();
        }

        public void SetStatusAsNotFound(string description)
        {
            context.SetStatusAsNotFound(description);
        }

        public long ContentLength
        {
            get
            {
                return context.ContentLength;
            }
            set
            {
                context.ContentLength = value;
            }
        }

        public string ContentType
        {
            get
            {
                return context.ContentType;
            }
            set
            {
                context.ContentType = value;
            }
        }

        public string ETag
        {
            get
            {
                return context.ETag;
            }
            set
            {
                context.ETag = value;
            }
        }

        public System.Net.WebHeaderCollection Headers
        {
            get { return context.Headers; }
        }

        public DateTime LastModified
        {
            get
            {
                return context.LastModified;
            }
            set
            {
                context.LastModified = value;
            }
        }

        public string Location
        {
            get
            {
                return context.Location;
            }
            set
            {
                context.Location = value;
            }
        }

        public System.Net.HttpStatusCode StatusCode
        {
            get
            {
                return context.StatusCode;
            }
            set
            {
                context.StatusCode = value;
            }
        }

        public string StatusDescription
        {
            get
            {
                return context.StatusDescription;
            }
            set
            {
                context.StatusDescription = value;
            }
        }

        public bool SuppressEntityBody
        {
            get
            {
                return context.SuppressEntityBody;
            }
            set
            {
                context.SuppressEntityBody = value;
            }
        }
    }
}
