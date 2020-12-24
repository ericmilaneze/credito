using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace Credito.Domain.Tests.DataAttributes
{
    public abstract class JsonFileDataAttribute : DataAttribute
    {
        private readonly string filePath;

        public JsonFileDataAttribute(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var fileData = GetFileData();
            return GetData(fileData);
        }

        protected abstract IEnumerable<object[]> GetData(string fileData);

        private string GetFileData()
        {
            var path = Path.IsPathRooted(filePath)
                            ? filePath
                            : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path))
                throw new ArgumentException($"Não foi possível encontrar o arquivo em: {path}");

            var fileData = File.ReadAllText(filePath);
            return fileData;
        }
    }
}