<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainProject.aspx.cs" Inherits="FineUIPro.Web.common.main2" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <link href="../res/index/css/reset.css" rel="stylesheet" />
    <link href="../res/index/css/home.css" rel="stylesheet" />
    <link href="../res/index/css/swiper-3.4.2.min.css" rel="stylesheet" />   
   <style type="text/css">
       .bg-item{
        position: relative;
        width: 100%;
        border: 2px solid rgb(0,162,233);
        border-radius: 16px;
        margin-bottom: 20px;
        box-shadow: 0 0 10px rgb(0,162,233);
       }
       .info{
           padding:10px;
       }
       .info .item{
        display: -webkit-flex;
        display: flex;
        display: -moz-box;
        display: -ms-flexbox;
        color:#fff;
        font-size:14px;
        letter-spacing:2px;
       }
       .info .item .tit{

       }
       .info .item .val{
        -webkit-flex: 1;
        -ms-flex: 1;
        -moz-box-flex: 1;
        min-width:0;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
       }
       .height260{
           height:260px;
       }
       .bw-item-content{
        padding:10px;
       }
        .tab-wrap {
            
        }
        .tab-small{
            left: auto;
            right: 15px;
            top:5px;
        }
        .tab-small .tab .t-item{
            width:auto;
            font-size:12px;
        }
        .bg-item-fix{
            padding:0 10px 10px;box-sizing:border-box;
        }
        .widthper{
            width:50%;
            box-sizing:border-box;
            
        }
        .itemfix{
            margin-bottom:25px;
        }
        .height130{
            height:130px;
        }

        /*将中间地图变成监控背景图*/
        .bg-img {
            displayb:block;
            height:100%;
            width:100%;
            border-radius:16px;
            /*background: url(../images/Page-01.jpg) center center no-repeat;
            background-size: 100% 100%;*/
        }
        .flexV{
            -webkit-box-orient: vertical;
            -webkit-flex-direction: column;
            -ms-flex-direction: column;
            flex-direction: column;
        }
        .wrap{
            padding: 15px 0;
            height:100%;
            box-sizing:border-box;
        }
        .bottom-wrap-new{
            height:100%;
        }
        .bg-item:last-child{
            margin-bottom: 0;
        }
         @media screen and (max-height: 625px) {
            .itemfix{
                margin-bottom:0;
            }
        }
        @media screen and (min-height: 625px) {
            .itemfix{
                margin-bottom:25px;
            }
        }
        .bw-b-bottom-up{
            height:100%;
        }
    </style>
</head>
<body >
    <div class="wrap">
        <div class="bottom-wrap-new flex">
            <!--左侧-->
            <div class="bw-s flex1 flex flexV">
                <div class="bg-item">
                    <div class="info">
                        <div class="row">
                            <div class="item">
                                <div class="tit">项目名称：</div>
                                <div class="val">测试项目</div>
                            </div>
                            <div class="item">
                                <div class="tit">项目地址：</div>
                                <div class="val">盘原地XXX弄XX号</div>
                            </div>                          
                            <div class="item">
                                <div class="tit">监理单位：</div>
                                <div class="val">单位二</div>
                            </div>
                             <div class="item">
                                <div class="tit">总承包单位：</div>
                                <div class="val">单位三</div>
                            </div>
                            <div class="item">
                                <div class="tit">施工分包单位：</div>
                                <div class="val">单位四</div>
                            </div>
                            <div class="item">
                                <div class="tit">总承包单位：</div>
                                <div class="val">单位五</div>
                            </div>
                              <div class="item">
                                <div class="tit">总承包单位：</div>
                                <div class="val">单位六</div>
                            </div>
                             <div class="item">
                                <div class="tit">项目合同额：</div>
                                <div class="val">1.28亿</div>
                            </div>
                            <div class="item">
                                <div class="tit">合同开工时间：</div>
                                <div class="val">2020-01-02</div>
                            </div>
                            <div class="item">
                                <div class="tit">合同竣工时间：</div>
                                <div class="val">2022-01-02</div>
                            </div>
                            <div class="item">
                                <div class="tit">项目类型：</div>
                                <div class="val">EPC</div>
                            </div>
                             <div class="item">
                                <div class="tit">项目状态：</div>
                                <div class="val">在建</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bg-item flex2">
                    <div class="tab-wrap tab-small">
                        <div class="tab" data-value="1">
                            <div class="t-item active">工程进度</div>
                            <div class="spline"></div>
                            <div class="t-item ">施工产值</div>
                        </div>
                    </div>
                    <div class="bw-item-content">
                        <div id='two' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
                <div class="bg-item flex1">
                    <div class="info">
                        <div class="row">
                            <div class="item itemfix">
                                <div class="tit">当天在场总人数：</div>
                                <div class="val">20人</div>
                            </div>
                            <div class="item itemfix">
                                <div class="tit">作业人员总人数：</div>
                                <div class="val">20人</div>
                            </div>
                            <div class="item">
                                <div class="tit">管理人员总人数：</div>
                                <div class="val">20人</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--中间-->
            <div class="bw-b flex2 flex flexV">
                <%--<div class="bw-b-bottom flex2">--%>
                    <div class="bw-b-bottom-up flex2">
                        <img src="../res/images/Page-01.jpg" alt="Alternate Text"  class="bg-img"/>
                        <div class="tab-wrap">
                            <div class="tab" data-value="0">
                                <div class="t-item ">监控</div>
                                <div class="spline"></div>
                                <div class="t-item active">平面图</div>
                            </div>
                        </div>
                        <%--<div id='map' style="width: 100%; height: 100%;"></div>--%>
                    </div>
                <%--</div>--%>
                <div class="bw-b-bottom flex flex1">
                    <div class="flex1 widthper" style="margin-right:10px">
                        <div class="bg-item bg-item-fix">
                        <div class="swiper-container" id='swiper1' style="height: 180px;">
                            <ul class="content-ul swiper-wrapper">
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于加强全国两会期间安全防范工作</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于组织开展全国“两会”期间安全质量环保专项督导检查</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">赛鼎公司2020年第一次安全生产委员会会议纪要</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">转发集团公司2020年安全工作会议精神</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于传达贯彻4月10日全国安全生产电视电话会议主要精神的通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于做好2020年项目复（开）工的通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                            </ul>
                        </div>
                    </div>
                    </div>
                    <div class="flex1 widthper">
                        <div class="bg-item bg-item-fix">
                        <div class="swiper-container" id='swiper2' style="height: 180px;">
                            <ul class="content-ul swiper-wrapper">
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于加强全国两会期间安全防范工作</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于组织开展全国“两会”期间安全质量环保专项督导检查</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">赛鼎公司2020年第一次安全生产委员会会议纪要</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">转发集团公司2020年安全工作会议精神</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于传达贯彻4月10日全国安全生产电视电话会议主要精神的通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于做好2020年项目复（开）工的通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                                <li class="c-item swiper-slide">
                                    <div class="tit">关于贯彻落实疫情防控和进一步做好安全生产工作的有关通知</div>
                                    <!-- <div class="time">2020-02-09</div> -->
                                </li>
                            </ul>
                        </div>
                    </div>
                    </div>
                    
                </div>
            </div>
            <!--右侧-->
            <div class="bw-s flex1 flex flexV">
                <div class="bg-item height260">
                    <div class="tab-wrap tab-small">
                        <div class="tab" data-value="3">
                            <div class="t-item active">质量验收</div>
                            <div class="spline"></div>
                            <div class="t-item ">质量问题</div>
                        </div>
                    </div>
                    <div class="bw-item-content">
                        <div id='three' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
                <div class="bg-item height260">
                    <div class="tab-wrap tab-small">
                        <div class="tab" data-value="4">
                            <div class="t-item active">安全人工时</div>
                            <div class="spline"></div>
                            <div class="t-item ">安全隐患</div>
                        </div>
                    </div>
                    <div class="bw-item-content">
                        <div id='four' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
                <div class="bg-item height130">
                    <div class="tab-wrap tab-small">
                        <div class="tab" data-value="5">
                            <div class="t-item active">待定</div>
                            <div class="spline"></div>
                            <div class="t-item ">待定</div>
                        </div>
                    </div>
                    <div class="bw-item-content">
                        <div id='five' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</body>
<script type="text/javascript" src="../res/index/js/jquery-3.4.1.min.js"></script>
<script type="text/javascript"  src="../res/index/js/swiper-3.4.2.jquery.min.js"></script>
<script type="text/javascript"  src="../res/index/js/echarts.min.js"></script>
<script type="text/javascript"  src="../res/index/js/china.js"></script>
<script type="text/javascript"  src="../res/index/js/world.js"></script>
<script type="text/javascript">
    function category_Two(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '进度统计',
                textStyle: {
                    fontWeight:'normal',
                    fontSize: 16,
                    color: '#fff'
                },
                show: true
            },
            tooltip: {},
            legend: {
                //data: ['销量'],
                show: false
            },
            xAxis: {
                axisTick: {
                    show: false
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    }
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255, 255, 255, 0.8)'
                    }
                },
                type: 'category',
                data: xArr,
                boundaryGap: [0, 0.01],
            },
            yAxis: {
                axisTick: {
                    show: false
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    }
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255, 255, 255, 0.8)'
                    },
                    formatter: function (value, index) {
                        return (value * 100) + "%";
                    }
                }
            },
            series: data,
            grid: {
                top: '15%',
                left: '0%',
                right: '0%',
                bottom: '0%',
                containLabel: true,
                backgroundColor: 'rgba(0,162,233, 0.01)',
                // borderColor: 'rgba(0,162,233, 1)'
            },
            backgroundColor: 'rgba(0,162,233, 0.01)',
            textStyle: {
                color: 'rgba(255, 255, 255, 0.3)'
            }
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.clear();
        myChart.setOption(option)
    }
    var xArr = ["分包一", "分包二", "分包三"]
    var data = [
        {
            name: '累计计划值',
            type: 'line',
            smooth: true,
            data: [0.23, 0.58, 1],
            //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
        },
        {
            name: '累计实际值',
            type: 'line',
            smooth: true,
            data: [0.2, 0.48, 0.83],
            //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
        }
    ]
    category_Two('two', xArr, data)
</script>


<script>
    function pie(id, data, title) {
        var myChartPie = echarts.init(document.getElementById(id));

        optionPie = {
            title: {
                text: title,
                textStyle: {
                     fontSize:16,
                     fontWeight:'normal',
                    color: '#fff'
                }
                , left: 0
                , top: 0
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                show: false,
                data: ['已整改', '未整改'],
                color: ['#32A8FF', ' #02C800'],
                orient: 'vertical',
                x: 'right',
                y: 'top',
                textStyle: {
                    color: ['#32A8FF', ' #02C800']
                }
            },
            //graphic: {
            //    type: "text",
            //    left: "center",
            //    top: "center",
            //    style: {
            //        text: "进度80%",
            //        textAlign: "center",
            //        fill: "#fff",
            //        fontSize: 18,
            //        fontWeight: 700
            //    }
            //},
            series: [
                {
                    name: '整改情况',
                    label: {
                        show: true,
                        textStyle: {
                            color: '#fff'
                        }
                    },
                    type: 'pie',
                    radius: ['45%', '60%'],
                    // avoidLabelOverlap:false,
                    color: ['#32A8FF', ' #02C800'],
                    data: [{
                        value: 43535, name: '已整改', itemStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(1, 0, 0, 0, [{ //颜色渐变函数 前四个参数分别表示四个位置依次为左、下、右、上

                                    offset: 0,
                                    color: '#508DFF'
                                }, {
                                    offset: 1,
                                    color: '#26C5FE'
                                }])
                            }
                        }
                    }, {
                        value: 5667, name: '未整改', itemStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(1, 0, 0, 0, [{ //颜色渐变函数 前四个参数分别表示四个位置依次为左、下、右、上

                                    offset: 0,
                                    color: '#63E587'
                                }, {
                                    offset: 1,
                                    color: '#5FE2E4'
                                }])
                            }
                        }
                    }]
                }
            ]
        };
        //为echarts对象加载数据
        myChartPie.clear()
        myChartPie.setOption(optionPie);
    }

     function pieTxt(id, title, text) {
        var myChartPie = echarts.init(document.getElementById(id));

        optionPie = {
            title: {
                text: title,
                textStyle: {
                     fontSize:16,
                     fontWeight:'normal',
                    color: '#fff'
                }
                , left: 0
                , top: 0
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                show: false,
                data: ['已整改', '未整改'],
                color: ['#32A8FF', ' #02C800'],
                orient: 'vertical',
                x: 'right',
                y: 'top',
                textStyle: {
                    color: ['#32A8FF', ' #02C800']
                }
            },
            graphic: {
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: text,
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 18,
                    fontWeight: 700
                }
            },
            series: [
                
            ]
        };
        //为echarts对象加载数据
        myChartPie.clear()
        myChartPie.setOption(optionPie);
    }
    pieTxt('four', "安全人工时", "800安全人工时")
</script>
<%--<script type="text/javascript">
    function randomData() {
        return Math.round(Math.random() * 1000);
    }
    var myChart = null
    function mapEchart(id, mapType, data) {
        myChart = echarts.init(document.getElementById(id));
        var mapType = mapType || 'china'
        optionMap = {
            title: {
                // x:"center",
                text: '这是监控',
                textStyle: {
                    fontSize: 20,
                    fontWeight: 'normal',
                    color: '#fff'
                    // color:'rgba(0,162,233)'
                }
                , left: "center"
                , top: 35
            },
            tooltip: {
                trigger: 'item'
                , formatter: '{b}<br>浏览量：{c}'
            },

            visualMap: {
                min: 0,
                max: 2500,
                left: 20,
                bottom: 10,
                text: ['高', '低'],// 文本，默认为数值文本
                // inRange: {
                //     color: ['#e0ffff', '#006edd']
                // },
                color: ['rgba(0, 64, 128, 0.1)', 'rgba(0, 64, 128,1)'],
                calculable: false,
                textStyle: {
                    color: '#fff'
                }
            },
            // geo: {
            //     map: mapType,
            //     roam: true,
            //     label: {
            //         show: true,
            //         color: 'rgba(0,0,0,1)'
            //     },
            //     itemStyle: {
            //         borderColor: 'rgba(0, 0, 0,1)'
            //     },
            //     emphasis:{
            //         itemStyle: {
            //             areaColor: null,
            //             shadowOffsetX: 0,
            //             shadowOffsetY: 0,
            //             shadowBlur: 20,
            //             borderWidth: 0,
            //             shadowColor: 'rgba(0, 0, 0, 1)'
            //         }
            //     }
            // },
            series: [
                {
                    type: 'map',
                    // mapType: 'world',
                    mapType: mapType,
                    roam: false,
                    data: data,
                    //地图区域的多边形 图形样式，有 normal 和 emphasis 两个状态
                    itemStyle: {
                        //normal 是图形在默认状态下的样式；
                        normal: {
                            show: true,
                            areaColor: "#66b2ff",
                            borderColor: "#ffffff",
                            borderWidth: "1",                            
                        },
                        //emphasis 是图形在高亮状态下的样式，比如在鼠标悬浮或者图例联动高亮时。
                        emphasis: {
                            show: true,
                            areaColor: "#0073e6",
                        }
                    },
                    //图形上的文本标签，可用于说明图形的一些数据信息
                    label: {
                        normal: {
                            show: false
                        },
                        emphasis: {
                            show: false
                        }
                    }
                }
            ]
        };
        //为echarts对象加载数据
        myChart.setOption(optionMap);
    }

    var data = [
        { name: 'China', value: randomData() },
        { name: 'United States', value: randomData() },
        { name: '北京', value: randomData() },
        { name: '天津', value: randomData() },
        { name: '上海', value: randomData() },
        { name: '重庆', value: randomData() },
        { name: '河北', value: randomData() },
        { name: '安徽', value: randomData() },
        { name: '新疆', value: randomData() },
        { name: '浙江', value: randomData() },
        { name: '江西', value: randomData() },
        { name: '山西', value: randomData() },
        { name: '内蒙古', value: randomData() },
        { name: '吉林', value: randomData() },
        { name: '福建', value: randomData() },
        { name: '广东', value: randomData() },
        { name: '西藏', value: randomData() },
        { name: '四川', value: randomData() },
        { name: '宁夏', value: randomData() },
        { name: '香港', value: randomData() },
        { name: '澳门', value: randomData() }
    ];

    mapEchart('map', 'china', data)

    //定义全国省份的数组
    var provinces = ['shanghai', 'hebei', 'shanxi', 'neimenggu', 'liaoning', 'jilin', 'heilongjiang', 'jiangsu', 'zhejiang', 'anhui', 'fujian', 'jiangxi', 'shandong', 'henan', 'hubei', 'hunan', 'guangdong', 'guangxi', 'hainan', 'sichuan', 'guizhou', 'yunnan', 'xizang', 'shanxi1', 'gansu', 'qinghai', 'ningxia', 'xinjiang', 'beijing', 'tianjin', 'chongqing', 'xianggang', 'aomen'];
    var provincesText = ['上海', '河北', '山西', '内蒙古', '辽宁', '吉林', '黑龙江', '江苏', '浙江', '安徽', '福建', '江西', '山东', '河南', '湖北', '湖南', '广东', '广西', '海南', '四川', '贵州', '云南', '西藏', '陕西', '甘肃', '青海', '宁夏', '新疆', '北京', '天津', '重庆', '香港', '澳门'];

    myChart.on('click', function (param) {
        // alert(param.name)
        // debugger
        //遍历取到provincesText 中的下标  去拿到对应的省js
        for (var i = 0; i < provincesText.length; i++) {
            if (param.name == provincesText[i]) {
                //显示对应省份的方法
                showProvince(provinces[i], provincesText[i])
                break
            }
        }
    });

    //展示对应的省
    function showProvince(pName, pRealName) {
        //这写省份的js都是通过在线构建工具生成的，保存在本地，需要时加载使用即可，最好不要一开始全部直接引入。 
        loadBdScript('$' + pName + 'JS', '../res/index/js/province/' + pName + '.js', function () {
            //初始化echarts:具体代码参考上面初始化中国地图即可，这里不再重复。
            initEcharts(pName, pRealName)
        });
    }
    //加载对应的JS
    function loadBdScript(scriptId, url, callback) {
        var script = document.createElement("script")
        script.type = "text/javascript";
        if (script.readyState) {  //IE  
            script.onreadystatechange = function () {
                if (script.readyState == "loaded" || script.readyState == "complete") {
                    script.onreadystatechange = null;
                    callback();
                }
            };
        } else {  //Others  
            script.onload = function () {
                callback();
            };
        }
        script.src = url;
        script.id = scriptId;
        document.getElementsByTagName("head")[0].appendChild(script);
    }

    function initEcharts(pName, pRealName) {

        optionMap = {
            title: {
                // x:"left",
                text: pRealName,
                textStyle: {
                    fontSize: 20,
                    fontWeight: 'normal',
                    color: '#fff'
                    // color:'rgba(0,162,233)'
                }
                , left: "center"
                , top: 35
            },
            tooltip: {
                trigger: 'item'
                , formatter: '{b}<br>浏览量：{c}'
            },

            visualMap: {
                min: 0,
                max: 2500,
                left: 20,
                bottom: 10,
                text: ['高', '低'],// 文本，默认为数值文本
                color: ['rgba(255,0,0,0.1)', 'rgba(255,0,0,1)'],
                calculable: false,
                textStyle: {
                    color: '#fff'
                }
            },
            series: [
                {
                    type: 'map',
                    mapType: pRealName,
                    roam: false,
                    data: [
                        { name: '合肥市', value: randomData() },
                        { name: '芜湖市', value: randomData() },
                        { name: '蚌埠市', value: randomData() },
                        { name: '淮南市', value: randomData() },
                        { name: '马鞍山市', value: randomData() },
                        { name: '淮北市', value: randomData() },
                        { name: '铜陵市', value: randomData() },
                        { name: '黄山市', value: randomData() },
                        { name: '滁州市', value: randomData() },
                        { name: '阜阳市', value: randomData() },
                        { name: '宿州市', value: randomData() },
                        { name: '亳州市', value: randomData() },
                        { name: '池州市', value: randomData() },
                        { name: '宣城市', value: randomData() }
                    ],
                    //地图区域的多边形 图形样式，有 normal 和 emphasis 两个状态
                    itemStyle: {
                        //normal 是图形在默认状态下的样式；
                        normal: {
                            show: true,
                            areaColor: "#00a2e9",
                            borderColor: "#FCFCFC",
                            borderWidth: "1"
                        },
                        //emphasis 是图形在高亮状态下的样式，比如在鼠标悬浮或者图例联动高亮时。
                        emphasis: {
                            show: true,
                            areaColor: "#C8A5DF",
                        }
                    },
                    //图形上的文本标签，可用于说明图形的一些数据信息
                    label: {
                        normal: {
                            show: false
                        },
                        emphasis: {
                            show: false
                        }
                    }
                }
            ]
        };
        //为echarts对象加载数据
        myChart.setOption(optionMap);
    }

</script>--%>
<script type="text/javascript">
    $(".tab .t-item").click(function () {
        var $this = $(this)
        var index = $this.index()
        if ($this.hasClass('active') && index == 0) {
            return
        }
        var $tab = $this.closest(".tab")
        var value = $tab.attr("data-value")
        $tab.find(".t-item").removeClass('active');
        $this.addClass('active')
        if (value == 0) {
            var maptype = index == 0 ? 'world' : 'china'
            mapEchart('map', maptype)
        }else if (value == 1) {
            if (index == 0) {
                var xArr = ["分包一", "分包二", "分包三"]
                var data = [
                    {
                        name: '累计计划值',
                        type: 'line',
                        smooth: true,
                        data: [0.23, 0.58, 1],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    },
                    {
                        name: '累计实际值',
                        type: 'line',
                        smooth: true,
                        data: [0.2, 0.48, 0.83],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    }
                ]
                category_Two('two', xArr, data)
            } else if (index == 2) {
                var dataX = ['施工已完产值', '项目施工合同总额']
                var data = [20, 40,]
                var dataT = [100, 100]
               echartsBarInit('two', "进度统计", dataX, data, dataT);
            }
        }
        else if (value == 2) {
            var xArr = ["分包一", "分包二", "分包三"]
            var data = [
                {
                    name: '计划值',
                    type: 'bar',
                    data: [0.23, 0.35, 0.42],
                    //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                },
                {
                    name: '实际值',
                    type: 'bar',
                    data: [0.2, 0.28, 0.35],
                    //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                },
                {
                    name: '累计计划值',
                    type: 'line',
                    smooth: true,
                    data: [0.23, 0.58, 1],
                    //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                },
                {
                    name: '累计实际值',
                    type: 'line',
                    smooth: true,
                    data: [0.2, 0.48, 0.83],
                    //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                }
            ]
            if (index == 2) {
                xArr = ["单位工程一", "单位工程二", "单位工程三"]
                data = [
                    {
                        name: '计划值',
                        type: 'bar',
                        data: [0.20, 0.33, 0.47],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    },
                    {
                        name: '实际值',
                        type: 'bar',
                        data: [0.15, 0.25, 0.33],
                        //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                    },
                    {
                        name: '累计计划值',
                        type: 'line',
                        smooth: true,
                        data: [0.20, 0.53, 1],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    },
                    {
                        name: '累计实际值',
                        type: 'line',
                        smooth: true,
                        data: [0.15, 0.4, 0.73],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    }
                ]
            }
            else if (index == 4) {
                xArr = ["建筑", "安装"]
                data = [
                    {
                        name: '计划值',
                        type: 'bar',
                        data: [0.45, 0.55],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    },
                    {
                        name: '实际值',
                        type: 'bar',
                        data: [0.36, 0.43],
                        //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                    },
                    {
                        name: '累计计划值',
                        type: 'line',
                        smooth: true,
                        data: [0.45, 1],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    },
                    {
                        name: '累计实际值',
                        type: 'line',
                        smooth: true,
                        data: [0.36, 0.79],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    }
                ]
            }
            category_Two('two', xArr, data)
        }
        else if (value == 3) {
            if (index == 0) {
                var dataX = ['质量验收一次合格率', '施工资料同步率']
                var data = [20, 60]
                var dataT = [100, 100]
                echartsBarInit('three', "质量验收", dataX, data, dataT);
            } else if (index == 2) {
                data = [{ value: 35, name: '质量不合格' },
                    { value: 65, name: '质量缺陷' }];
                pie('three', data, "质量问题")
            }
            //var data = [{ value: 10, name: '分包一' },
            //{ value: 5, name: '分包二' },
            //{ value: 15, name: '分包三' },
            //{ value: 25, name: '分包四' },
            //{ value: 20, name: '分包五' },
            //{ value: 35, name: '分包六' }];
            //if (index == 2) {
            //    data = [{ value: 25, name: '单位工程一' },
            //    { value: 35, name: '单位工程二' },
            //    { value: 30, name: '单位工程三' }];
            //}
            //else if (index == 4) {
            //    data = [{ value: 45, name: '建筑' },
            //    { value: 55, name: '安装' }];
            //}
            //else if (index == 6) {
            //    data = [{ value: 35, name: '质量不合格' },
            //    { value: 65, name: '质量缺陷' }];
            //}
            
        }
        else if (value == 4) {
            if (index == 0) {
                pieTxt('four', "安全人工时", "1000安全人工时")
            } else if (index == 2) {
                 data = [{ value: 35, name: '质量不合格' },
                    { value: 65, name: '质量缺陷' }];
                pie('four', data, "安全隐患")
            }
            
        }
    })
</script>
<script>
    var dataX = ['质量验收一次合格率', '施工资料同步率']
    var data = [20, 60]
    var dataT = [100, 100]
    echartsBarInit('three', "质量验收", dataX, data, dataT);
    function echartsBarInit(id, title, dataX, data, dataT) {
        var myChart = echarts.init(document.getElementById(id))   // 初始化echarts实例
        myChart.clear();
        myChart.setOption(// 通过setOption来生成柱状图
            {
                title: {
                    // left:'center',
                    text: title,
                    textStyle: {
                        color: '#fff',
                        fontSize:16,
                        fontWeight:'normal',
                    },
                    show: true
                },
                grid: {   // 直角坐标系内绘图网格
                    left: '0',  //grid 组件离容器左侧的距离,
                    //left的值可以是80这样具体像素值，
                    //也可以是'80%'这样相对于容器高度的百分比
                    top: '25',
                    right: '0',
                    bottom: '0',
                    containLabel: true   //gid区域是否包含坐标轴的刻度标签。为true的时候，
                    // left/right/top/bottom/width/height决定的是包括了坐标轴标签在内的
                    //所有内容所形成的矩形的位置.常用于【防止标签溢出】的场景
                },
                xAxis: {  //直角坐标系grid中的x轴,
                    //一般情况下单个grid组件最多只能放上下两个x轴,
                    //多于两个x轴需要通过配置offset属性防止同个位置多个x轴的重叠。
                    type: 'value',//坐标轴类型,分别有：
                    //'value'-数值轴；'category'-类目轴;
                    //'time'-时间轴;'log'-对数轴
                    splitLine: { show: false },//坐标轴在 grid 区域中的分隔线
                    axisLabel: { show: false },//坐标轴刻度标签
                    axisTick: { show: false },//坐标轴刻度
                    axisLine: { show: false },//坐标轴轴线
                },
                yAxis: {
                    type: 'category',
                    axisTick: { show: false },
                    axisLine: { show: false },
                    axisLabel: {
                        color: '#fff',
                        // fontSize: 12
                    },
                    data: dataX//类目数据，在类目轴（type: 'category'）中有效。
                    //如果没有设置 type，但是设置了axis.data,则认为type 是 'category'。
                },
                series: [//系列列表。每个系列通过 type 决定自己的图表类型
                    {
                        name: '%',//系列名称
                        type: 'bar',//柱状、条形图
                        barWidth: 19,//柱条的宽度,默认自适应
                        data: data,//系列中数据内容数组
                        label: { //图形上的文本标签
                            show: true,
                            position: 'right',//标签的位置
                            offset: [0, -20],  //标签文字的偏移，此处表示向上偏移40
                            formatter: '{c}{a}',//标签内容格式器 {a}-系列名,{b}-数据名,{c}-数据值
                            color: '#fff',//标签字体颜色
                            fontSize: 12  //标签字号
                        },
                        itemStyle: {//图形样式
                            normal: {  //normal 图形在默认状态下的样式;
                                //emphasis图形在高亮状态下的样式
                                barBorderRadius: 10,//柱条圆角半径,单位px.
                                //此处统一设置4个角的圆角大小;
                                //也可以分开设置[10,10,10,10]顺时针左上、右上、右下、左下
                                color: new echarts.graphic.LinearGradient(
                                    0, 0, 1, 0,
                                    [{
                                        offset: 0,
                                        color: '#22B6ED'
                                    },
                                    {
                                        offset: 1,
                                        color: '#3FE279'
                                    }
                                    ]
                                )
                            }
                        },
                        zlevel: 1//柱状图所有图形的 zlevel 值,
                        //zlevel 大的 Canvas 会放在 zlevel 小的 Canvas 的上面
                    },
                    {
                        name: '进度条背景',
                        type: 'bar',
                        barGap: '-100%',//不同系列的柱间距离，为百分比。
                        // 在同一坐标系上，此属性会被多个 'bar' 系列共享。
                        // 此属性应设置于此坐标系中最后一个 'bar' 系列上才会生效，
                        //并且是对此坐标系中所有 'bar' 系列生效。
                        barWidth: 19,
                        data: dataT,
                        color: '#151B87',//柱条颜色
                        itemStyle: {
                            normal: {
                                barBorderRadius: 10
                            }
                        }
                    }
                ]
            }
        )
    }
</script>


<script>        
    var mySwiper = new Swiper('#swiper1', {
        //autoplay: 3000,//可选选项，自动滑动
        direction: 'vertical',
        loop: false,
        slidesPerView: 6
    })

    var mySwiper = new Swiper('#swiper2', {
        //autoplay: 4000,//可选选项，自动滑动
        direction: 'vertical',
        loop: false,
        slidesPerView: 6
    })
</script>
</html>
