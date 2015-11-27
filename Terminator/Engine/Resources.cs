using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    class Resources
    {
        private List<String> textureName = new List<String>();
        private List<int> textureId = new List<int>();

        public int getTextureId(string textureName)
        {
            for (int i = 0; i < this.textureName.Count; i++)
            {
                if (this.textureName[i].Equals(textureName))
                    return textureId[i];
            }
            return -1;
        }

        public void setTextureId(string textureName, int textureId)
        {
            this.textureName.Add(textureName);
            this.textureId.Add(textureId);
        }
    }
}
