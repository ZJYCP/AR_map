using System.Collections;      
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class modelcontr : MonoBehaviour, ITrackableEventHandler
{

    [SerializeField]
    #region PROTECTED_MEMBER_VARIABLES
    // Audio&BgMusic：绑定的gameobject
    // public AudioSource Audio;
    
    // public AudioSource BgMusic;


    public GameObject board;  //那个显示的框框

    public GameObject textcontent;  //框框里说明的文字
    Text text;
    public GameObject model;  //模型

    // GameObject myScrollRect;

    

    public GameObject title;   //框框的大标题

    Text titletext;

    public GameObject musictest;  //BgMusic对象

    // 提取出的配音和背景乐
    AudioSource Audio2;
    AudioSource BgMusic2;

    // BgMusic对象上的AudioSource组件
    AudioSource Audio3;
    AudioSource BgMusic3;

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        

        //音乐监听事件
        // musicbutton=GameObject.Find("/Canvas/music").GetComponent<Button>();
        // musicbutton.onClick.AddListener(closemusic);
        
        titletext=title.GetComponent<Text>();
        text = textcontent.GetComponent<Text>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    public virtual void OnTrackingFound()
    {
        textcontent.transform.localPosition = new Vector3(0,0,0);  //设置框框的说明文字回到顶部

        // 启用模型？？
        for (int i = 0; i < transform.childCount; i++)
        {
            var go=transform.GetChild(i).gameObject;
            go.SetActive(true);
        }

        // 音乐对象的转移，方面后续播放暂停操作，我只能想到这么low的办法
        var source_music=GameObject.Find("Audio").GetComponents(typeof(AudioSource));
        Audio2=(AudioSource)source_music[0];
        BgMusic2=(AudioSource)source_music[1];
        var middle_music=musictest.GetComponents(typeof(AudioSource));
        Audio3=(AudioSource)middle_music[0];
        BgMusic3=(AudioSource)middle_music[1];
        Audio3.clip=Audio2.clip;
        BgMusic3.clip=BgMusic2.clip;

       
        Audio3.Play(); //配音
        BgMusic3.Play();  //背景乐
        UIcontrol.get_img();  //UI变化

        //变换框里的显示文字
        var objectname=this.gameObject.name;
        if (objectname=="Shanxi")
        {
            titletext.text="平遥古城";
            text.text="平遥古城旧称古陶，位于山西省中部平遥县内，始建于西周宣王时期，目的是防御外族南扰。它被称为“保存最为完好的四大古城”之一，也是中国仅有的以整座古城申报世界文化遗产获得成功的两座古城市之一。平遥城墙总周长6163米，墙高约12米，把面积约2.25平方公里的平遥县城一隔为两个风格迥异的世界。城墙以内街道、铺面、市楼保留明清形制，城墙以外称新城。平遥古城是中国汉民族城市在明清时期的杰出范例，在中国历史的发展中，为人们展示了一幅非同寻常的汉族文化、社会、经济及宗教发展的完整画卷。";
        }else if (objectname=="Beijing")
        {
            titletext.text="天安门";
            text.text="天安门，坐落在中华人民共和国首都北京市的中心、故宫的南端，与天安门广场以及人民英雄纪念碑、毛主席纪念堂、人民大会堂、中国国家博物馆隔长安街相望。天安门是明清两代北京皇城的正门，始建于明朝永乐十五年，最初名为'承天门'，寓意着'承天启运、受命于天'，清朝顺治八年才更名为天安门。天安门由城台和城楼两部分组成，城楼长66米、宽37米。城台下有券门五阙，中间的券门最大，位于北京皇城中轴线上，过去只有皇帝才可以由此出入。现在它的正中门洞上方悬挂着毛主席画像，两边分别是'中华人民共和国万岁'和'世界人民大团结万岁'的大幅标语。小朋友们，期待你们的到来哦！";
        }else if (objectname=="xinjiang")
        {
            titletext.text="新疆大巴扎";
            text.text="新疆国际大巴扎位于新疆乌鲁木齐市，是世界规模最大的大巴扎，在维吾尔语中，巴扎意为集市、农贸市场。大巴扎集伊斯兰文化、建筑、民族商贸、娱乐、餐饮于一体，是“新疆之窗”、“中亚之窗”和“世界之窗”。它具有浓郁的伊斯兰建筑风格，重现了古丝绸之路的繁华，体现出了浓郁的西域民族特色和地域文化。同时啊它也是新疆商业与旅游繁荣的象征呢！大巴扎作为乌鲁木齐的少数民族城市的景观建筑，也成为了乌鲁木齐的一座标志性建筑。";
        }else if (objectname=="guangdong")
        {
            titletext.text="广州塔";
            text.text="广州塔又称广州新电视塔，位于中国广州市海珠区赤岗塔附近，昵称小蛮腰或水蛇腰，嘻嘻小朋友们觉得像吗？广州塔塔身主体高450米，加上150米的天线桅杆，总高度达到了600米 ，具有结构性超高、造型奇特、形体复杂，用钢量最多的特点。它的外框筒由24根钢柱和46个钢椭圆环交叉构成，形成镂空、开放的独特美体，它仿佛在三维空间中扭转变换。作为目前世界上建筑物腰身最细，施工难度最大的建筑，建设者们克服了前所未有的工程建筑难度，把一万多个倾斜并且大小规格全部不相同的钢构件，精确安装成挺拔高耸的建筑经典作品，并创造了一系列建筑上的'世界之最'";
        }else if (objectname=="Hongkong")
        {
            titletext.text="中银大厦";
            text.text="中银大厦全称为中国银行（香港）大厦，位于中国香港。这座楼高70层，一共315米，曾经在1989年至1992年间是香港及全亚洲最高的建筑物。现在就不在是了，因为更高的楼被建造出来了呢。\n这座大楼的设计灵感来源于竹子的'节节高升'，象征着力量、生机、茁壮和锐意进取的精神，也寓意中国银行(香港)未来蓬勃发展。基座的麻石做的外墙代表长城，代表中国，而且还用玻璃幕进行覆盖，就像是将中国的传统建筑意念和现代的先进建筑科技结合起来。大厦由四个不同高度结晶体般的三角柱身组成，呈多面棱形，好比璀璨生辉的水晶体，在阳光照射下呈现出不同的色彩，想想就很美呢。";
        }else if (objectname=="shanghai")
        {
            titletext.text="东方明珠";
            text.text="东方明珠广播电视塔位于上海市浦东新区陆家嘴，是上海标志性文化景观之一，也是每位游客来到上海的必去之地。这座塔高约468米，发射天线桅杆长约110米，承担上海6套无线电视发射业务，地区覆盖半径80公里。\n这座电视塔是多筒结构的。仔细观察一下它的结构，我们可以发现它的主干是由3根空心的擎天大柱组成的。它还有顶、上、下三个球体。顶球体被称为太空厅，共有4层，包含会议厅、观光层、管道层等。上球体共有9层，含有空中KTV包房、旋转餐厅、瞭望平台等。下球体共有4层，含有室内游乐场。";
        }else if (objectname=="fujian")
        {
            titletext.text="福建土楼";
            text.text="福建土楼包括闽南土楼和客家土楼，产生于宋朝、元朝时期，成熟于明朝末期、清朝和民国时期。2008年第32届世界遗产大会上被正式列入《世界遗产名录》，是我国珍贵的文化遗产，需要我们每一个人的共同守护哦。\n福建土楼是以土、木、石、竹为主要建筑材料，利用未经烧焙的土并按一定比例的沙质黏土和黏质沙土拌合而成，用夹墙板夯筑而成的两层以上的房屋。\n福建土楼属于集体性建筑，造型庞大，足足可以容纳几百人一起居住呢。它的形状各种各样。像是圆形、半圆形、方形、四角形、五角形、交椅形、畚箕形等形状，各具特色。";
        }else if (objectname=="taiwan")
        {
            titletext.text="台北101";
            text.text="台北101大楼由国际级建筑大师李祖原精心设计，以中国人的吉祥数字'八'('发'的谐音)，作为设计单元。每八层形成一组自主构成的空间，自然化解由高层建筑引起的气流对地面造成的风场效应，透过建筑设计绿化植栽区的区隔，来确保行人的安全与舒适性。你看这座大楼，造型宛若劲竹节节高升、柔韧有馀，象征生生不息的中国传统建筑意涵。内斜七度的建筑面，层层往上递增。在外观上形成有节奏的律动美感，开创了国际摩天楼新风格。";
        }else if (objectname=="neimenggu")
        {
            titletext.text="蒙古包";
            text.text="蒙古包是蒙古族牧民居住的一种房子。蒙古包呈圆形尖顶，顶上和四周以一至两层厚毡覆盖。 蒙古包的包门开向东南，你知道为什么吗？因为这样既可避开西伯利亚的强冷空气，也可以沿袭着以日出方向为吉祥的古老传统。蒙古包看起来外形虽小，但包内使用面积却很大，而且室内空气流通，采光条件好，冬暖夏凉，不怕风吹雨打，所以非常适合于经常转场放牧民族居住和使用呢。而现在在中国，随着蒙古族的游牧习俗变成了定点放牧，蒙古族人民几乎完全定居在砖瓦房或楼房里啦。我们一般只能在旅游区才能见到传统意义上的蒙古包了。";
        }else if (objectname=="Tibet")
        {
            titletext.text="藏式佛塔";
            text.text="藏式佛塔是西藏一种非常独特的艺术形式。佛塔的地宫一般在地面以下，主要用来储存高僧的舍利、法器以及生前的遗物，还有各种珍宝。塔基在整个地宫以上，它是整座塔的基础。塔身呢，就是整个塔的主体，如果你想分别各种不同类型的塔，就是从它的塔身形状来判别。而最上端就是塔刹，塔刹上面有各种具有佛教意义的装饰。刹顶的月亮象征的是“气息”，或者是“风”；太阳象征的是“精神”，或者是“灵气”。而且你知道吗？藏式佛塔其实还有很多种样式，可能这些艺术形式繁复而琐碎，但是藏民追求单一、纯洁的心灵是我们有目共睹而感动的。";
        }
        // scrollcontent.verticalNormalizedPosition=1.0f;
        board.SetActive(true);
        
        //动画设置，不一定需要
        model.GetComponent<Animator>().SetBool("upup",true);

            
    }


    protected virtual void OnTrackingLost()
    {

        // 动画设置
        var stateplay=model.GetComponent<Animator>();
        if (stateplay.isActiveAndEnabled){
            model.GetComponent<Animator>().SetBool("upup",false);
        }
        
        // 同上上
        if(Audio3){
            Audio3.Stop();
            BgMusic3.Stop();
            UIcontrol.lost_img();
        }

    

        for (int i = 0; i < transform.childCount; i++)
        {
            var go=transform.GetChild(i).gameObject;
            go.SetActive(false);
        }

        board.SetActive(false);

        
    }

    #endregion // PROTECTED_METHODS

}




