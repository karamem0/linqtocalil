﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace LinqToCalil.Tests {

    /// <summary>
    /// <see cref="LinqToCalil.Calil.GetCheck"/> をテストします。
    /// </summary>
    [TestClass()]
    public class CalilCheckContextTest {

        public string ApplicationKey { get; set; }

        [TestInitialize()]
        public void TestInitialize() {
            this.ApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
        }

        /// <summary>
        /// OrElse 演算子を使用して蔵書検索を実行します。
        /// </summary>
        [TestMethod()]
        public void CheckAsEnumerable1() {
            var target = Calil.GetCheck(this.ApplicationKey);
            var actual = target
                .Where(x => x.SystemId == "Tokyo_Ota")
                .Where(x => x.Isbn == "403217010A" || x.Isbn == "4032171009")
                .AsEnumerable(r => {
                    Debug.WriteLine("Polling:" + DateTime.Now.ToString());
                    foreach (var item in r) {
                        Debug.WriteLine(item);
                    }
                    return true;
                });
            Assert.IsNotNull(actual);
            var result = actual.ToList();
            Assert.IsNotNull(result);
            Debug.WriteLine("Completed:" + DateTime.Now.ToString());
            foreach (var item in result) {
                Debug.WriteLine(item);
            }
        }

        /// <summary>
        /// カンマ区切りで指定して蔵書検索を実行します。
        /// </summary>
        [TestMethod()]
        public void CheckAsEnumerable2() {
            var target = Calil.GetCheck(this.ApplicationKey);
            var actual = target
                .Where(x => x.SystemId == "Tokyo_Ota")
                .Where(x => x.Isbn == "403217010X,4032171009")
                .AsEnumerable(r => {
                    Debug.WriteLine("Polling:" + DateTime.Now.ToString());
                    foreach (var item in r) {
                        Debug.WriteLine(item);
                    }
                    return true;
                });
            Assert.IsNotNull(actual);
            var result = actual.ToList();
            Assert.IsNotNull(result);
            Debug.WriteLine("Completed:" + DateTime.Now.ToString());
            foreach (var item in result) {
                Debug.WriteLine(item);
            }
        }

    }

}
