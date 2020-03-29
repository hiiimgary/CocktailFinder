using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace CocktailFinder
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var nullBulider = new NullBuilder();
            var nullOrderer = new NullOrderer();

            BundleResolver.Current = new CustomBundleResolver();
            var commonStyleBundle = new CustomStyleBundle("~/Bundle/sass");

            commonStyleBundle.Include("~/Styles/main.scss");
            commonStyleBundle.Orderer = nullOrderer;
            bundles.Add(commonStyleBundle);
        }
    }
}
