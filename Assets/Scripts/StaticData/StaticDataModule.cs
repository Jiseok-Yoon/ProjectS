using ProjectS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ProjectS.SD
{
    /// <summary>
    /// ��� ��ȹ �����͸� ��� �ִ� Ŭ����
    /// �����͸� �ε��ϰ� ��� �ֱ⸸ �� ���̹Ƿ� ��븦 ��ӹ��� �ʿ䰡 ����
    /// </summary>
    // ��븦 ���� �ʴ� �Ϲ� C# Ŭ������ �ν����Ϳ� �����Ű�� ���� ����ȭ
    [Serializable]
    public class StaticDataModule
    {
        public List<SDString> sdString = new List<SDString>();

        /// <summary>
        /// �δ��� ���� ��ȹ �����͸� �ҷ��ɴϴ�.
        /// </summary>
        public void Initialize()
        {
            var loader = new StaticDataLoader();
            loader.Load<SDString>(out sdString);
        }

        /// <summary>
        /// ��ȹ�����͸� �ҷ��� �δ�
        /// </summary>
        private class StaticDataLoader
        {
            private string path;

            public StaticDataLoader()
            {
                path = $"{Application.dataPath}/StaticData/Json";
            }

            public void Load<T>(out List<T> data) where T : StaticData
            {
                // �����̸��� Ÿ���̸����� SD�� �����ϸ� �����ϴٴ� ��Ģ�� ����..
                var fileName = typeof(T).Name.Remove(0, "SD".Length);

                var json = File.ReadAllText($"{path}/{fileName}.json");

                data = SerializationUtil.FromJson<T>(json);
            }
        }
    }
}
