<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainProject.aspx.cs" Inherits="FineUIPro.Web.common.mainProject" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <link href="../res/index/css/reset.css" rel="stylesheet" />
    <link href="../res/index/css/home.css" rel="stylesheet" />
    <link href="../res/index/css/swiper-3.4.2.min.css" rel="stylesheet" />   
   <style type="text/css">
       .row2 .item{
           height:100%;
       }
       .spline-mid{
           width: 1px;
           height: 80%;
           border-left: 1px solid #2779AA;
           margin-top: 5%;
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
        padding:0px;
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
            width:100%;
            height:100%;
            padding:0 10px 10px;
            box-sizing:border-box;
        }
        .mbnone{
            margin-bottom: 0;
        }
        .widthper{
            width:48%;
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
            
        }
        .bg-img-jk{
            background: url(../res/images/Page-01.jpg) center center no-repeat;
            background-size: 100% 100%;
        }
        .bg-img-jk-1{
            background: url(../res/images/enable_gzip_iis6_5.png) center center no-repeat;
            background-size: 100% 100%;
        }
        .wrap{
            padding: 10px 0 0;
            height:100%;
            box-sizing:border-box;
        }
        .bottom-wrap-new{
            height:100%;
        }
        .swiperHeightWrap{
            height:200px;
        }
        .swiperHeight{
            height:120px;
        }
         @media screen and (max-height: 625px) {
            .swiperHeightWrap{
                height:200px;
            }
            .swiperHeight{
                height:120px;
            }
            .itemfix{
                margin-bottom:0;
            }
        }
        @media screen and (min-height: 625px) {
            .swiperHeightWrap{
               height:320px;
            }
            .swiperHeight{
               height:170px;
            }
            .itemfix{
                margin-bottom:25px;
            }
        }
        .bw-b-bottom-up{
            height:100%;
            margin-bottom: 0!important;
        }
         .tab-wrap .tab .t-item{
            width:auto;
            background-color:rgb(23, 68, 122) !important;
        }
        .tab-wrap .tab .active{
             background-color:rgba(0,162,233, 1) !important;
        }
        
        .tab-content .line-item{
            background-color:#0B5EA5;
            border-radius: 10px;
            height:10px;
        }
         .tab-content .line-item>div{
            background-color:#29D8DD;
         }
        .txt-board{
            align-items:center;
            justify-content: space-around;

        }
        .num-wrap{
            background-color:#0b5089;
            border-radius:10px;
            padding: 5px;
            /*min-height: 90px;*/
            height: 70%;
            justify-content:center;
            align-items:center;
            margin:5px;
        }
        .num-wrap .tit{
            color:#fff;
            margin: 10px 0 0;
            font-size: 11px;
            width:100%;
            text-align:center;
        }
        .num-wrap .num{
            align-items:center;
            justify-content: space-around;
            color:#fff;
            width:100%;
            text-align:center;
        }
        .num-wrap .num>div{
            background-color:#387491;
            color:#FFB84D;
            padding: 2px;
            font-size:24px;
            margin:5px;
        }
        .num-wrap .color1>div{
             color:#19c719;
        }
        .num-wrap .color2>div{
             color:#0000ff;
        }
        .bg-item{
             height:100%;
        }
        .itemflex{
           padding-bottom:10px;
        }
        .itemflex2{
             padding-top:10px;
        }
        .bw-b-bottom{
            width:100%;
            height:100%;
        }
    </style>
</head>
<body >
    <div class="wrap">
        <div class="bottom-wrap-new flex">
            <!--左侧-->
            <div class="bw-s flex1 flex flexV">
                <div class="flex1 itemflex">
                    <div class="bg-item">
                        <div class="bw-item-content flex flexV">
                            <div class="tit-new">安全数据统计</div>
                            <div class="content-wrap flex1 flex flexV" style="padding:0 10px;">
                                <div class="row1 flex">
                                    <div>安全人工时：</div><span class="num">1432345</span><div>小时</div>
                                </div>
                                <div class="row2 flex1 flex">
                                    <div class="item flex1">
                                        <div id="one2" style="width: 100%; height: 100%;"></div>
                                    </div>
                                    <div class="item flex1 flex flexV" style="align-items:center;justify-content:center;">
                                      <div class="item-txt-list flex">
                                        <div class="txt-tit">总数量：</div>
                                        <div>43</div>
                                      </div>
                                      <div class="item-txt-list flex">
                                        <div class="txt-tit fixtt">待整改：</div>
                                        <div>10</div>
                                      </div>
                                      <div class="item-txt-list flex">
                                        <div class="txt-tit fixtt">已整改：</div>
                                        <div>33</div>
                                      </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="content-wrap flex1 flex">
                                <div class="flex1" id='one1' style="width: 100%; height: 100%;"></div>
                                <div class="spline-mid"></div>
                                <div class="flex1" id='one2' style="width: 100%; height: 100%;"></div>
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div class="flex1 itemflex">
                    <div class="bg-item ">
                        <div class="bw-item-content flex flexV">
                            <div class="tit-new">质量一次验收合格率</div>
                            <div id='two' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
                <div class="flex1 itemflex">
                    <div class="bg-item ">
                        <div class="bw-item-content flex flexV">
                            <div class="tit-new">焊接数据统计</div>
                            <%--<div id='three' style="width: 100%; height: 100%;"></div>--%>
                            <div class="content-wrap flex1 flex">
                                <div class="flex1" id='three1' style="width: 100%; height: 100%;"></div>
                                <div class="spline-mid"></div>
                                <div class="flex1" id='three2' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--中间-->
            <div class="bw-b flex2 flex flexV">
                <div class="flex2 ">
                    <div class="bw-b-bottom-up">
                        <div class="info js-item-1" style="display:none">
                            <div class="row" style="margin-top:50px;">
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
                        <div style="display:none" class="bg-img js-item-1 bg-img-jk-1"></div>
                        <div class="bg-img js-item-1 bg-img-jk"></div>
                        <%--<img style="display:none" src="../res/images/enable_gzip_iis6_5.png" alt="Alternate Text"  class=""/>
                        <img  src="../res/images/Page-01.jpg" alt="Alternate Text"  class="bg-img js-item-1"/>--%>
                        <div class="tab-wrap">
                            <div class="tab" data-value="0">
                                <div class="t-item ">项目概况</div>
                                <div class="spline"></div>
                                <div class="t-item">总平面图</div>
                                <div class="spline"></div>
                                <div class="t-item active">监控</div>
                            </div>
                        </div>
                        <%--<div id='map' style="width: 100%; height: 100%;"></div>--%>
                    </div>
                </div>
                <div class="flex1 itemflex itemflex2">
                <div class="bw-b-bottom flex">
                    <div class="flex1 widthper mbnone"  id="swiper-pre" style="flex: 1; width: 48%;">
                        <div class="flex1 flex flexV bg-item bg-item-fix pd0">
                            <div class="tit-new">
                                <div class="tab-wrap-tit">
                                    <div class="tab" data-value="1">
                                        <div class="t-item active">待办</div>
                                        <div class="spline"></div>
                                        <div class="t-item">预警</div>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-container pdlrb" id='swiper1' style="width:100%;">
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
                    <div class="spline" style="width:2%;"></div>
                    <div class="flex1 widthper mbnone" style="flex: 1; width: 48%;">
                        <div class="flex1 flex flexV bg-item bg-item-fix pd0">
                            <div class="tit-new">通知</div>
                            <div class="swiper-container pdlrb" id='swiper2' style="width:100%;">
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
            </div>
            <!--右侧-->
            <div class="bw-s flex1 flex flexV">
                 <div class="flex1 itemflex">
                 <div class="bg-item" >
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">劳务数据统计</div>
                        <%--总人数--%>
                        <div class="content-wrap flex1 flex txt-board">
                            <div class="num-wrap flex1 flex flexV">
                                <div class="tit">当前现场人数</div>
                                <div class="num flex flex1">
                                    <div class="flex1">5</div>
                                    <div class="flex1">1</div>
                                    <div class="flex1">9</div>
                                </div>
                            </div>
                             <div class="num-wrap flex1 flex flexV">
                                <div class="tit">作业人员总数</div>
                                <div class="num flex flex1 color1">
                                    <div class="flex1">3</div>
                                    <div class="flex1">0</div>
                                    <div class="flex1">1</div>
                                </div>
                            </div>
                            <div class="num-wrap flex1 flex flexV">
                                <div class="tit">管理人员总数</div>
                                <div class="num flex flex1 color2">
                                    <div class="flex1">2</div>
                                    <div class="flex1">7</div>
                                    <div class="flex1">2</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                 <div class="flex1 itemflex">
                <div class="bg-item">
                     <div class="bw-item-content flex flexV">
                        <div class="tit-new">进度统计</div>
                        <div class="content-wrap flex1 flex">
                            <div  id='four' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
                </div>
                 <div class="flex1 itemflex">
                <div class="bg-item">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">合同统计</div>
                        <div class="content-wrap tab-content flex1" style="overflow:auto;">
                            <div class="flex tab-h">
                                <div class="txt">分包合同</div>
                                <div class="txt">分包商</div>
                                <div class="flex1">合同完成百分比</div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">合同1</div>
                                <div class="txt">二化建</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div style="width:50%"></div>
                                    </div>
                                    <div class="per">50%</div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">合同2</div>
                                <div class="txt">分包商</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div style="width:90%"></div>
                                    </div>
                                    <div class="per">90%</div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">合同3</div>
                                <div class="txt">分包商</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div style="width:90%"></div>
                                    </div>
                                    <div class="per">90%</div>
                                </div>
                            </div>
                            <%--<div class="flex tab-i">
                                <div class="txt">合同4</div>
                                <div class="txt">分包商</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div style="width:90%"></div>
                                    </div>
                                    <div class="per">90%</div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
                </div>
            </div>

        </div>
    </div>
</body>
<script type="text/javascript" src="../res/index/js/jquery-3.4.1.min.js"></script>
<script type="text/javascript"  src="../res/index/js/swiper-3.4.2.jquery.min.js"></script>
<script type="text/javascript"  src="../res/index/js/echarts.min.js"></script>
<script type="text/javascript">
    function category_One(id, title, dataNum) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            //tooltip: {
            //    formatter: '{a} <br/>{b} : {c}%'
            //},
            title: {
                bottom: '0',
                left:'center',
                text: title,
                textStyle: {
                    color: '#fff',
                    fontSize: 10,
                    fontWeight:'300'
                },
                show: true
            },
            series: [
                {
                    name: ' ',
                    center: ["50%", "50%"],
                    type: 'gauge',
                    radius: "90%",
                    axisLabel : { //文字样式（及“10”、“20”等文字样式）
                        fontSize: 10,
                        distance : 5 //文字离表盘的距离
                    },
                    pointer: {
                        show: true,
                        length: '70%',
                        width : 3
                    },
                    axisTick : { //刻度线样式（及短线样式）
                      length : 0
                    },
                    splitLine: {
                        length: 10,
                        lineStyle: {
                            color: 'rgba(255,255,255,.1)'
                        }
                    },
                    axisLine: {
                        lineStyle: {
                             color : [ //表盘颜色
                                [ 0.5, "#91C7AE" ],//0-50%处的颜色
                                [ 0.7, "#63869E" ],//51%-70%处的颜色
                                [ 1, "#88C8E2" ],//70%-100%处的颜色
                            ],
                            width : 10//表盘宽度
                        }
                    },
                    min: 0,
                    max: 100,
                    detail: {
                        show: false,
                        formatter: '{value}%'
                    },
                    data: [{
                        value: dataNum,
                        name: ''
                    }]
                }
            ]
        };
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option, true)
    }
    //category_One('one1', '安全人工时统计', 80)
    category_One('three1', '项目焊接一次合格率', 80)
    category_One('three2', '项目焊接进度完成率', 80)
</script>
<script>
    function pie(id, data, title, text) {
        var myChartPie = echarts.init(document.getElementById(id));
        var optionPie = {
            //tooltip: {
            //    trigger: 'item',
            //    show: false
            //},
            legend: {
                show: false,
                selectedMode: false,
                left: 'right',
                orient: 'horizontal',
                textStyle: {//图例文字的样式
                    color: '#ffffff'
                }
            },
            title: {
                left: 'center',
                bottom: '0',
                text: title,
                textStyle: {
                    color: '#fff',
                    fontSize: 10,
                    fontWeight:'300'
                },
                show: false
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
            color: ['#2566CF', '#289CB3'],
            series: [
                {
                    name: ' ',
                    hoverOffset: 0,
                    type: 'pie',
                    clickable:false,
                    radius: ['55%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false
                    },
                    itemStyle: {
                        normal: {
                            //opacity: 0.7,
                            borderWidth: 3,
                            borderColor: 'rgba(137,214,243, 1)'
                        }
                    },
                    emphasis: {
                        label: {
                            show: true,
                            fontSize: '12',
                            fontWeight: 'bold'
                        }
                    },
                    labelLine: {
                        show: false
                    },
                    data: data
                }
            ]
        };
        //为echarts对象加载数据
        myChartPie.setOption(optionPie);
    }
    var data = [{ value: 33, name: '' },
    { value: 10, name: '' }];
    //pie('one2', data, "安全隐患整改统计", "43")
    pie('one2', data, "", "43")
</script>
<script type="text/javascript">
    function category(id, xArr, series) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '',
                textStyle: {
                    color: '#fff',
                    fontWeight: 'normal',
                    fontSize:16
                },
                show: false
            },
            tooltip: {},
            legend: {
                left: '3%',
                show: false,
                selectedMode: false,
                textStyle:{//图例文字的样式
                    color:'#ffffff'
                }
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
                data: xArr
            },
            yAxis: {
                axisTick: {
                    show: false
                },
                axisLine: {
                    show: false,
                    lineStyle: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    }
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255, 255, 255, 0.8)'
                    }
                }
            },
            series: series,
            grid: {
                top: '12%',
                left: '10',
                right: '10',
                bottom: '10',
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
        myChart.setOption(option)
    }
    var xArr = ["单位工程1", "单位工程2", "单位工程3", "单位工程4", "单位工程5", "单位工程6", "单位工程7", "单位工程8", "单位工程9"]
    var data = [12, 5, 28, 43, 22, 11, 40, 21, 9]
    var data1 = [21, 9, 12, 15, 8, 43, 17, 11, 22]
    var series = [{
        name: '质量一次性合格率',
        type: 'bar',
        data: data,
        itemStyle: { normal: { color: 'rgba(43,155,176,1)' } }
    }];
    category('two', xArr, series)
</script>
<script type="text/javascript">
    function line(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'center',
                text: ' ',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
                },
                show: false
            },
            tooltip: {},
            legend: {
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
                data: xArr
            },
            yAxis: {
                axisTick: {
                    show: false
                },
                axisLine: {
                    show: false,
                    lineStyle: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    }
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255, 255, 255, 0.8)'
                    }
                }
            },
            series: data,
            grid: {
                top: '15%',
                left: '10',
                right: '10',
                bottom: '0%',
                containLabel: true,
                backgroundColor: 'rgba(0,162,233, 0.01)',
                // borderColor: 'rgba(0,162,233, 1)'
            },
            itemStyle: {
                //color: 'rgba(200,201,10, 1)'
            },
            backgroundColor: 'rgba(0,162,233, 0.01)',
            textStyle: {
                color: 'rgba(255, 255, 255, 0.3)'
            }
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
    var xArr = ["11.07", "11.08", "11.09", "11.10", "11.11", "11.12", "11.13", "11.14", "11.15", "11.16", "11.17", "11.18"]
    var data = [ {
        name: '项目安全人工时',
        type: 'line',
        //smooth: true,
        data: [3, 5, 2, 3, 4, 2, 9, 8, 4, 7, 6, 1],
        lineStyle: {
            //color: 'rgba(200,201,10, 1)'
        }
    },{
        name: '项目安全人工时1',
        type: 'line',
        //smooth: true,
        data:  [6, 5, 3, 3, 4, 1, 3, 2, 4, 7, 6, 1]
    },{
        name: '项目安全人工时2',
        type: 'line',
        //smooth: true,
        data:  [5, 4, 2, 3, 8, 2, 9, 2, 4, 1, 6, 4]
    }]
    //line('three', xArr, data)
    var xArr1 = ['类别1', '类别2', '类别3', '类别4', '类别5', '类别6', '类别7']
    var data1 = [ {
        name: '1',
        type: 'line',
        //smooth: true,
        data: [3, 5, 2, 3, 4, 2, 9],
        lineStyle: {
            //color: 'rgba(200,201,10, 1)'
        }
    },{
        name: '2',
        type: 'line',
        //smooth: true,
        data:  [1, 3, 2, 4, 7, 6, 1]
    }]
    line('four', xArr1, data1)
</script>
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
    //category_Two('two1', xArr, data)
</script>



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
            $(".js-item-1").hide()
            if (index == 0) {
                $(".js-item-1").eq(0).show()
            } else if (index == 2) {
                $(".js-item-1").eq(1).show()
            } else if (index == 4) {
                $(".js-item-1").eq(2).show()
            }
        }else if (value == 1) {
            
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
                //echartsBarInit('three', "质量验收", dataX, data, dataT);
            } else if (index == 2) {
                data = [{ value: 35, name: '质量不合格' },
                    { value: 65, name: '质量缺陷' }];
                //pie('three', data, "质量问题")
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
    //echartsBarInit('three1', "质量验收", dataX, data, dataT);
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
    $(document).ready(function () {
        var height = $("#swiper-pre").height()
        $("#swiper1,#swiper2").css("height", (height -38) + 'px')
         var mySwiper = new Swiper('#swiper1', {
            //autoplay: 3000,//可选选项，自动滑动
            direction: 'vertical',
            loop: false,
            slidesPerView: 5
        })

        var mySwiper = new Swiper('#swiper2', {
            //autoplay: 4000,//可选选项，自动滑动
            direction: 'vertical',
            loop: false,
            slidesPerView: 5
        })
    })
   
</script>
</html>
