<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="FineUIPro.Web.common.main" %>

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
        .bw-item-content {
            padding: 0;
        }
       
        .bw-item {
            height: auto;
            margin: 0 0 10px 0;
        }
        .bw-item:last-child {
            margin-bottom: 0;
        }

        .wrap {
            padding: 10px 0;
            height: 100%;
            box-sizing: border-box;
        }

        .bottom-wrap {
            height: 100%;
            box-sizing: border-box;
        }

        .bw-b-bottom-up {
            height: 100%;
        }
        .bw-b-bottom {
            height: 120px;
        }
        .bw-b, .bw-b-bottom {
            box-sizing: border-box;
        }

        .mb {
            margin-bottom: 10px;
        }

        .mr {
            margin-right: 10px;
        }

        .mbnone {
            margin-bottom: 0;
        }

        .swiperHeightWrap {
            height: 170px;
        }

        .swiperHeight {
            height: 90px;
        }
        .content-wrap{
            padding:0  20px 20px;
        }
        @media screen and (max-height: 625px) {
            .swiperHeightWrap {
                height: 170px;
            }

            .swiperHeight {
                height: 90px;
            }
            .content-wrap{
                padding:0  10px 10px;
            }
        }

        @media screen and (min-height: 625px) {
            .swiperHeightWrap {
                height: 300px;
            }

            .swiperHeight {
                height: 200px;
            }
        }
       
        .link{
            color:#fff;
        }
        .map-desc{
            position:absolute;
            right:10px;
            top:50%;
            transform:translateY(-50%);
        }
  
        .map-desc .desc{
           
            color:#00a2e9;
            /*text-shadow: 0 0 10px #7a7cd0,0 0 20px #7a7cd0,0 0 30px #7a7cd0,0 0 40px #7a7cd0;*/	/*设置发光效果*/
           text-shadow: 0 0 10px #00a2e9;
        }
         .map-desc .desc .d-item{
            /*border-top: 1px solid #00a2e9;
            border-bottom: 1px solid #00a2e9;*/
            padding: 3px 10px;
             /*background-color: rgba(1, 30, 50 ,0.3);*/
             display:flex;
             flex-direction:column;
             align-items:center;
             margin-bottom:5px;
             position:relative;
         }
         .map-desc .desc .d-item:last-child{
              margin-bottom:0;
         }
          .map-desc .desc .d-item:before, .map-desc .desc .d-item:after{
              content: '';
              display:inline-block;
              width:12px;
              height:12px;
              position:absolute;
          }
           .map-desc .desc .d-item:before{
               width:100%;
                top:0;
                left:0;
              border-top: 1px solid #00a2e9;
              border-left: 1px solid #00a2e9;
           }
           .map-desc .desc .d-item:after{
                width:100%;
               bottom:0;
               right:0;
              border-bottom: 1px solid #00a2e9;
              border-right: 1px solid #00a2e9;
           }
         .map-desc .desc .d-item-1{
            /*background-color: rgba(0,53,97 ,0.3) !important;*/
        }
        .map-desc .desc .d-item .tit{
            font-size:10px;
        }
         .map-desc .desc .d-item .num{
             font-size:18px;
        }
        .project-wrap {
            position:absolute;
            left:50%;
            top:0px;
            transform:translateX(-50%);
            color:#fff;
        }
        .project-wrap .project{
            border: 1px solid #004F88;
            position:relative;
            min-width:100px;
        } 
        .project-wrap .project-tit{
            padding: 5px 10px;
            color:#fff;
            border: 1px solid #004F88;
            background-color: #004F88;
            position:relative;
            min-width:100px;
        }
        .project-wrap .project:before{
            content: '';
            position:absolute;
            right: 0px;
            top: 4px;
            width: 8px;
            height:8px;
            border-top:1px solid #fff;
            border-right:1px solid #fff;
            transform:rotate(135deg);
        }
        .project-list{
            display:none;
            background-color:rgba(1,82,138, 0.8);
            color:#fff;
        }
         .project-list>div{
             padding: 5px 10px;
         }
         .project-list>div:hover{
            background-color:rgba(1,82,138, 0.9);
         }
          .tab-content .line-item{
            background-color:#0B5EA5;
            border-radius: 10px;
            height:10px;
        }
         .tab-content .line-item>div{
            background-color:#29D8DD;
         }
         .Accumulation-next .tab-h .txt,.Accumulation-next .tab-i .txt{
             width:45px;
         }
    </style>
</head>
<body>
    <div class="wrap">
        <div class="bottom-wrap flex">
            <!--左侧-->
            <div class="bw-s flex1 flex flexV">
                <div class="bg-item flex1">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">安全数据统计</div>
                        <div class="content-wrap flex1 flex flexV" style="padding-bottom:0;">
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
                <div class="bg-item flex1">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">质量一次验收合格率</div>
                        <div id='two' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
                <div class="bg-item flex1">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">焊接数据统计</div>
                        <div id='three' style="width: 100%; height: 100%;"></div>
                    </div>
                </div>
            </div>
            <!--中间 -->
            <div class="bw-b flex2 flexV flex">
                <div class="flex2 ">
                    <div class="bw-b-bottom-up">
                        <div class="tab-wrap">
                            <div class="tab" data-value="0">
                                <div class="t-item ">境外</div>
                                <div class="spline"></div>
                                <div class="t-item active">境内</div>
                            </div>
                        </div>
                        <div id='map' style="width: 100%; height: 100%;"></div>
                        <div class="project-wrap">
                            <div class="project">
                                <div>
                                    <input autocomplete="off" class="project-tit" type="text" name="name" value="" />
                                </div>
                                <div class="project-list">
                                    <div>项目1</div>
                                    <div>项目2</div>
                                    <div>项目3</div>
                                </div>
                            </div>
                            <%--<f:DropDownList runat="server" Width="200px" ID="drpProject" OnSelectedIndexChanged="drpProject_SelectedIndexChanged"
                                AutoPostBack="true" EnableEdit="true">
                            </f:DropDownList>--%>
                        </div>
                        <div class="map-desc">
                            <div class="desc">
                                <div class="d-item">
                                    <div class="tit">工地总数</div>
                                    <div class="num">1，231</div>
                                </div>
                                <div  class="d-item d-item-1">
                                    <div class="tit">在建</div>
                                    <div class="num">35</div>
                                </div>
                                <div  class="d-item d-item-1">
                                    <div class="tit">停工</div>
                                    <div class="num">12</div>
                                </div>
                                <div  class="d-item">
                                    <div class="tit">竣工</div>
                                    <div class="num">59</div>
                                </div>
                                <div  class="d-item">
                                    <div class="tit">单位：(个)</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="flex flex1" style="margin-top:10px;">
                    <div id="swiper-pre" class="bw-item flex1 mbnone" style="flex: 1; width: 48%;">
                        <div class="bw-item-content flex flexV">
                            <div class="tit-new">劳务统计</div>
                            <%--<div class=" flex1">
                                <div id='Accumulation' style="width: 100%; height: 100%;"></div>
                            </div>--%>
                            <div class="content-wrap tab-content flex1 flex" style="overflow:auto;">
                                <div class="Accumulation-next">
                                    <div class="flex tab-h">
                                        <div class="txt">工程名</div>
                                        <div class="txt">在线数</div>
                                        <%--<div class="flex1">在线率</div>--%>
                                    </div>
                                    <div class="flex tab-i">
                                        <div class="txt">工程1</div>
                                        <div class="txt">12</div>
                                       <%-- <div class="flex1 flex line-wrap">
                                            <div class="line-item">
                                                <div style="width:50%"></div>
                                            </div>
                                            <div class="per">50%</div>
                                        </div>--%>
                                    </div>
                                    <div class="flex tab-i">
                                        <div class="txt">工程2</div>
                                        <div class="txt">23</div>
                                        <%--<div class="flex1 flex line-wrap">
                                            <div class="line-item">
                                                <div style="width:90%"></div>
                                            </div>
                                            <div class="per">90%</div>
                                        </div>--%>
                                    </div>
                                    <div class="flex tab-i">
                                    <div class="txt">工程3</div>
                                    <div class="txt">34</div>
                                   <%-- <div class="flex1 flex line-wrap">
                                        <div class="line-item">
                                            <div style="width:90%"></div>
                                        </div>
                                        <div class="per">90%</div>
                                    </div>--%>
                                </div>
                                </div>
                                <div class=" flex1">
                                    <div id='Accumulation' style="width: 100%; height: 100%;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="spline" style="width: 2%;"></div>
                    <div class="bw-item flex1 mbnone" style="flex: 1; width: 48%;">
                        <div class="bw-item-content flex flexV">
                            <div class="tit-new">
                                <div class="tab-wrap-tit">
                                    <div class="tab" data-value="1">
                                        <div class="t-item active">通知</div>
                                        <div class="spline"></div>
                                        <div class="t-item">待办</div>
                                    </div>
                                </div>
                            </div>
                            <div class="" style="padding:10px 10px 0;">
                                <div class="swiper-container " id='swiper2'>
                                     <ul class="content-ul swiper-wrapper">
                                        <li class="c-item swiper-slide">
                                            <div class="tit">关于加强全国两会期间安全防范工作</div>
                                        </li>
                                         <li class="c-item swiper-slide">
                                        <div class="tit">关于组织开展全国“两会”期间安全质量环保专项督导检查</div>
                                        <!-- <div class="time">2020-02-09</div> swiperHeight-->
                                        </li>
                                        <li class="c-item swiper-slide">
                                            <div class="tit">赛鼎公司2020年第一次安全生产委员会会议纪要</div>
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
            <div class="bw-s flex1 flexV flex" style="">
                <div class="bg-item flex1">
                    <%--<div class="bw-item-content flex flexV">
                        <div class="tit-new">进度统计</div>
                        <div class="content-wrap flex1">
                            <div id='echartsBar' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>--%>
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">进度统计</div>
                        <div class="content-wrap tab-content flex1" style="overflow:auto;">
                            <div class="flex tab-h">
                                <div class="txt">工地名称</div>
                                <div class="txt">状态</div>
                                <div class="flex1" style="text-align:center">进度</div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地1</div>
                                <div class="txt">正常</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="normal" style="width:80%"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地2</div>
                                <div class="txt">正常</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="normal" style="width:50%"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地3</div>
                                <div class="txt">超期</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="warn" style="width:100%"></div>
                                    </div>
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
                <div class="bg-item flex1">
                   <div class="bw-item-content flex flexV">
                        <div class="tit-new">产值/合同统计</div>
                        <div class="content-wrap flex1">
                            <div id='five' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
                <div class="bg-item flex1">
                   <%--<div class="bw-item-content flex flexV">
                        <div class="tit-new">劳务统计</div>
                        <div class="content-wrap flex1">
                            <div id='Accumulation' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>--%>
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">站点链接</div>
                        <div class="content-wrap tab-content flex1" style="overflow:auto;">
                            <a class="link">公司网站</a>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</body>
<script type="text/javascript" src="../res/index/js/jquery-3.4.1.min.js"></script>
<script type="text/javascript" src="../res/index/js/swiper-3.4.2.jquery.min.js"></script>
<script type="text/javascript" src="../res/index/js/echarts.min.js"></script>
<script type="text/javascript" src="../res/index/js/china.js"></script>
<script type="text/javascript" src="../res/index/js/world.js"></script>
    <script type="text/javascript">
    function category_One(id, dataNum) {
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
                text: '安全人工时统计',
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
                    radius: "80%",
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
    //category_One('one1', 80)
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
    function category(id) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '安全统计',
                textStyle: {
                    color: '#fff'
                },
                show: false
            },
            tooltip: {},
            legend: {
                data: ['数量'],
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
                data: ["已整改", "待整改", "巡检数", "专项检查", "综合检查", "隐患"]
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
                    }
                }
            },
            series: [{
                name: '销量',
                type: 'bar',
                data: [5, 20, 66, 10, 10, 20]
            }],
            grid: {
                top: '15',
                left: '0%',
                right: '0%',
                bottom: '0%',
                containLabel: true,
                backgroundColor: 'rgba(0,162,233, 0.01)',
                // borderColor: 'rgba(0,162,233, 1)'
            },
            itemStyle: {
                color: 'rgba(200,201,10, 1)'
            },
            backgroundColor: 'rgba(0,162,233, 0.01)',
            textStyle: {
                color: 'rgba(255, 255, 255, 0.3)'
            }
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }

    //category('main')
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
    //var xArr = ["单位工程1", "单位工程2", "单位工程3", "单位工程4", "单位工程5", "单位工程6", "单位工程7", "单位工程8", "单位工程9"]
    var xArr = ["项目1", "项目2", "项目3", "项目4", "项目5", "项目6", "项目7", "项目8", "项目9"]
    var data = [12, 5, 28, 43, 22, 11, 40, 21, 9]
    var data1 = [21, 9, 12, 15, 8, 43, 17, 11, 22]
    var series = [{
        name: '质量一次性合格率',
        type: 'bar',
        data: data,
        itemStyle: { normal: { color: 'rgba(43,155,176,1)' } }
    }];
    var series1 = [
    {
        name: '施工资料同步率',
        type: 'bar',
        data: data1,
        itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
    }];
    category('two', xArr, series)
    category('three', xArr, series1)
</script>
    <script type="text/javascript">
    function category_Five(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'center',
                text: '',
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
                color: 'rgba(200,201,10, 1)'
            },
            backgroundColor: 'rgba(0,162,233, 0.01)',
            textStyle: {
                color: 'rgba(255, 255, 255, 0.3)'
            }
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
    var xArr = ["类别1", "类别2", "类别3", "类别4", "类别5", "类别6", "类别7"]
    var data = [{
            name: '',
            type: 'bar',
            stack: '总量',
            data: [20, 2, 1, 34, 39, 30, 20],
            itemStyle: { normal: { color: 'rgba(160,181,204, 1)' } }
        },
        {
            name: '',
            type: 'bar',
            stack: '总量',
            data: [12, 32, 10, 14, 9, 30, 21],
            itemStyle: { normal: { color: 'rgba(28,110,173, 1)' } }
        }]
    category_Five('five', xArr, data)
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
</script>
<script>
    function line1(id, name) {
        var myChartLine = echarts.init(document.getElementById(id));
        optionLine = {
            title: {
                // left:'center',
                text: name,
                textStyle: {
                    color: '#fff'
                },
                show: false
            },
            tooltip: {
                trigger: 'axis'
            },
            lineStyle: {
                normal: {
                    color: '#32A8FF'
                }
            },
            // areaStyle:{
            //     normal:{
            //       //颜色渐变函数 前四个参数分别表示四个位置依次为左、下、右、上
            //         color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{ 

            //             offset: 0,
            //             color: 'rgba(80,141,255,0.39)'
            //         }, {
            //             offset: .34,
            //             color: 'rgba(56,155,255,0.25)'
            //         },{
            //             offset: 1,
            //             color: 'rgba(38,197,254,0.00)'
            //         }])

            //     }
            // },
            grid: {
                top: '15',
                left: '3%',
                right: '4%',
                bottom: '9%',
                containLabel: true
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
                boundaryGap: false,
                data: ['11.07', '11.08', '11.09', '11.10', '11.11', '11.12', '11.13', '11.14', '11.15', '11.16']
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
                    }
                },
                type: 'value'
                /*min:0,
                max:60,
                splitNumber:6*/
            },
            series: [
                {
                    name: '测试一',
                    type: 'line',
                    stack: '总量1',
                    // areaStyle: {normal: {}},
                    data: ['10', '22', '10', '30', '13', '31', '15', '10', '22', '10'],
                    itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                },
                {
                    name: '测试二',
                    type: 'line',
                    stack: '总量2',
                    data: ['5', '20', '13', '38', '3', '29', '12', '8', '25', '12'],
                    itemStyle: { normal: { color: 'rgba(0,162,233,.5)' } }
                },
                {
                    name: '测试三',
                    type: 'line',
                    stack: '总量3',
                    data: ['15', '30', '23', '48', '13', '39', '22', '18', '35', '22'],
                    itemStyle: { normal: { color: 'rgba(215,35,47,.9)' } }
                },
            ]
        }
        //为echarts对象加载数据
        myChartLine.setOption(optionLine);
    }

    var cqmsData =<%=CQMSData %>

        function cqms(id, name, data) {
            var myChartLine = echarts.init(document.getElementById(id));
            option = {
                title: {
                    top: 0,
                    // left:'center',
                    text: '质量统计',
                    textStyle: {
                        color: '#fff'
                    },
                    show: false
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'shadow',        // 默认为直线，可选为：'line' | 'shadow'
                        formatter: "{c}%"       //标签内容格式器 {a}-系列名,{b}-数据名,{c}-数据值
                    }
                },
                legend: {
                    show: false
                    //data: ['单位三', '单位二', '单位一']
                },
                grid: {
                    top: '15',
                    left: '6%',
                    right: '6%',
                    bottom: '0%',
                    containLabel: true
                },
                tooltip: {
                    show: true,
                    formatter: function (params) {
                        var id = params.dataIndex;
                        return params.name + '<br>质量验收一次合格率：' + params.data + '%';
                    }
                },
                xAxis: {
                    show: false,
                    splitLine: {
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
                    },
                    type: 'value'
                },
                yAxis: {
                    // offset: 10,

                    nameGap: 50,
                    axisTick: {
                        show: false
                    },
                    splitLine: {
                        show: false
                    },
                    axisLine: {
                        show: false,
                        lineStyle: {
                            color: 'rgba(255, 255, 255, 0.3)'
                        }
                    },
                    axisLabel: {
                        margin: 15,
                        show: true,
                        textStyle: {
                            color: 'rgba(255, 255, 255, 0.8)'
                        }
                    },
                    type: 'category',
                    data: cqmsData.categories
                },
                series: [
                    {
                        barWidth: 10, // 柱子宽度
                        //barGap:'80%',/*多个并排柱子设置柱子之间的间距*/
                        //barCategoryGap:'20%',/*多个并排柱子设置柱子之间的间距*/
                        name: data.series[0].name,
                        type: 'bar',
                        stack: '合格率',
                        label: {
                            show: true,
                            position: 'insideRight',
                            formatter: "{c}%"
                        },
                        data: data.series[0].data
                    }
                ]
            };
            //为echarts对象加载数据
            myChartLine.setOption(option);
        }
    //cqms('cqms', '质量统计', cqmsData)
    $(document).ready(function () {
        //line1('line1', '合同统计')
    })
</script>
<script>
    function pie(id) {
        var myChartPie = echarts.init(document.getElementById(id));
        optionPie = {
            title: {
                text: '统计进度',
                textStyle: {
                    // fontSize:14,
                    // fontWeight:'normal',
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
                    text: "进度80%",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 18,
                    fontWeight: 700
                }
            },
            series: [
                {
                    name: '整改情况',
                    label: {
                        show: false
                    },
                    type: 'pie',
                    radius: ['65%', '85%'],
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
        myChartPie.setOption(optionPie);
    }
    //pie('pie')
</script>
<script>
    function randomData() {
        return Math.round(Math.random() * 1000);
    }
    var myChart = null
    function mapEchart(id, mapType) {
        myChart = echarts.init(document.getElementById(id));
        var mapType = mapType || 'china'
        var mapZoom = mapType == 'china' ? 1.2 : 1
        optionMap = {
            title: {
                // x:"center",
                text: '',
                textStyle: {
                    fontSize: 15,
                    fontWeight: 'normal',
                    color: '#fff'
                    // color:'rgba(0,162,233)'
                }
                , left: "center"
                , top: 35
            },
            tooltip: {
                trigger: 'item'
                , formatter: '{b}<br>数量：{c}'
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
                    zoom: mapZoom,
                    roam: false,
                    data: [
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
                    ],
                    //地图区域的多边形 图形样式，有 normal 和 emphasis 两个状态
                    itemStyle: {
                        //normal 是图形在默认状态下的样式；
                        normal: {
                            show: true,
                            areaColor: "#66b2ff",
                            borderColor: "#FCFCFC",
                            borderWidth: "1"
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

    mapEchart('map', 'china')

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
                , formatter: '{b}<br>数量：{c}'
            },

            visualMap: {
                min: 0,
                max: 2500,
                left: 20,
                bottom: 10,
                text: ['高', '低'],// 文本，默认为数值文本
                color: ['rgba(0, 64, 128, 0.1)', 'rgba(0, 64, 128,1)'],
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
                            areaColor: "#1a75ff",
                            borderColor: "#FCFCFC",
                            borderWidth: "1"
                        },
                        //emphasis 是图形在高亮状态下的样式，比如在鼠标悬浮或者图例联动高亮时。
                        emphasis: {
                            show: true,
                            areaColor: "#0052cc",
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

</script>
<script>
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
        } else if (value == 1) {

        }
    })
</script>
<script>
    //echartsBarInit('echartsBar', jdglData)
    var jdglData =<%=JDGLData %>
        function echartsBarInit(id, data) {
            var myChart = echarts.init(document.getElementById(id))   // 初始化echarts实例
            myChart.setOption(// 通过setOption来生成柱状图
                {
                    title: {
                        // left:'center',
                        text: '进度统计',
                        textStyle: {
                            color: '#fff'
                        },
                        show: false
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
                        data: ['施工', '工程', '项目']//类目数据，在类目轴（type: 'category'）中有效。
                        //如果没有设置 type，但是设置了axis.data,则认为type 是 'category'。
                    },
                    series: [//系列列表。每个系列通过 type 决定自己的图表类型
                        {
                            name: '%',//系列名称
                            type: 'bar',//柱状、条形图
                            barWidth: 19,//柱条的宽度,默认自适应
                            data: [20, 40, 60],//系列中数据内容数组
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
                            data: [100, 100, 100],
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
    function Accumulation(id) {
        var myChartLine = echarts.init(document.getElementById(id));
        option = {
            title: {
                top: 0,
                // left:'center',
                text: '劳务统计',
                textStyle: {
                    color: '#fff'
                },
                show: false
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'cqms' | 'shadow'
                }
            },
            legend: {
                show: false,
                data: ['单位三', '单位二', '单位一']
            },
            grid: {
                top: '15',
                left: '0',
                right: '0',
                bottom: '0%',
                containLabel: true
            },
            xAxis: {
                show: false,
                splitLine: {
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
                },
                type: 'value'
            },
            yAxis: {
                // offset: 10,
                show: false,
                nameGap: 50,
                axisTick: {
                    show: false
                },
                splitLine: {
                    show: false
                },
                axisLine: {
                    show: false,
                    lineStyle: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    }
                },
                axisLabel: {
                    margin: 15,
                    show: true,
                    textStyle: {
                        color: 'rgba(255, 255, 255, 0.8)'
                    }
                },
                type: 'category',
                data: ['周一', '周二', '周三', '周四']
                //data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
            },
            series: [
                {
                    barWidth: 10, // 柱子宽度
                    //barGap:'80%',/*多个并排柱子设置柱子之间的间距*/
                    //barCategoryGap:'20%',/*多个并排柱子设置柱子之间的间距*/
                    name: '单位三',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
                    data: [320, 302, 301, 334]
                    //data: [320, 302, 301, 334, 390, 334, 390]
                },
                {
                    name: '单位二',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
                    data: [120, 132, 101, 134] //, 90, 334, 390
                },
                {
                    name: '单位一',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
                    data: [220, 182, 191, 234]//, 290, 334, 390
                }
            ]
        };
        myChartLine.setOption(option);
    }
    Accumulation('Accumulation')
</script>
<script>
    function radar(id) {
        var myChart = echarts.init(document.getElementById(id))
        option = {
            title: {
                text: '焊接统计',
                textStyle: {
                    color: '#fff'
                },
                show: false
            },
            tooltip: {},
            legend: {
                show: false,
                data: ['焊口数', '达因']
            },
            radar: {
                // shape: 'circle',
                name: {
                    textStyle: {
                        color: '#fff',
                        // backgroundColor: '#999',
                        borderRadius: 3,
                        padding: [3, 5]
                    }
                },
                indicator: [
                    { name: '焊口', max: 46500 },
                    { name: '焊接', max: 36000 },
                    { name: '点口', max: 8000 },
                    { name: '检测', max: 3000 },
                    { name: '返修', max: 100 }
                ]
            },
            series: [{
                name: '焊接 vs 点口',
                type: 'radar',
                // areaStyle: {normal: {}},
                data: [
                    {
                        value: [4300, 10000, 35000, 50000, 19000],
                        name: '焊口数'
                    },
                    {
                        value: [5000, 1400, 3100, 300, 100],
                        name: '达因'
                    }
                ]
            }]
        };
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
    //radar('radar')
</script>
<script>
    $(document).ready(function () {
        var height = $("#swiper-pre").height()
        $("#swiper2").css("height", (height -38) + 'px')
        var mySwiper = new Swiper('#swiper2', {
            autoplay: 4000,//可选选项，自动滑动
            direction: 'vertical',
            loop: true,
            slidesPerView: 3
        })
    })
    //var mySwiper = new Swiper('#swiper1', {
    //    autoplay: 3000,//可选选项，自动滑动
    //    direction: 'vertical',
    //    loop: true,
    //    slidesPerView: 3
    //})

   
</script>
</html>
