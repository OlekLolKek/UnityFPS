using System.Collections.Generic;
using UnityEngine;


public class VideoSettings
{
    #region Fields

    public int CurrentSettings { get; set; }    
    public List<VideoSettingsItems> Items { get; set; }

    #endregion
}

public struct VideoSettingsItems
{
    #region Fields

    public string Name { get; set; }
    public Resolution ScreenResolution { get; set; }
    public ShadowQuality ShadowQuality { get; set; }
    public bool SoftParticles { get; set; }

    #endregion


}
