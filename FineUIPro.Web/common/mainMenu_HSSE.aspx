<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainMenu_HSSE.aspx.cs" Inherits="FineUIPro.Web.mainMenu_HSSE" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <link href="../res/index/css/reset.css" rel="stylesheet" />
    <link href="../res/index/css/home.css" rel="stylesheet" />
    <link href="../res/index/css/swiper-3.4.2.min.css" rel="stylesheet" />
    <style type="text/css">
        * {
            box-sizing: border-box;
        }

        .flexV {
            flex-direction: column;
        }

        .wrap {
            height: 100%;
            padding: 15px;
        }

        .bottom-wrap {
            padding: 0;
            margin-bottom: 5px;
        }

            .bottom-wrap:last-child {
                margin-bottom: 0;
            }

        .bw-item-content {
            padding: 5px;
        }

        .top {
            display: flex;
            display: -webkit-flex;
            overflow: hidden;
            width: 100%;
        }

        .bw-b-bottom {
            width: 100%;
            height: 100%;
        }

        .itemlr {
            margin: 0 5px;
        }

        .bw-b-bottom-up {
            border-radius: 0;
            height: 100%;
            margin: 0;
            box-shadow: none;
        }

        .yj-info {
            align-items: center;
            justify-content: center;
            width: 100%;
            height: 100%;
            color: red;
            font-size: 20px;
        }

            .yj-info .tit {
                font-weight: 700;
                margin-bottom: 20px;
            }

            .yj-info .tel {
                font-weight: 700;
            }

        .tab-wrap {
            left: auto;
            right: 15px;
            top: 5px;
        }

        .tab .t-item {
            width: auto;
            padding: 5px 10px;
        }

        .bottom-list {
            height: 100px;
            padding: 15px;
            overflow: hidden;
            color: #fff;
            margin: 0 10px;
        }

            .bottom-list .bl-left {
                float: left;
                margin-right: 30px;
                margin-left: 50px;
            }

            .bottom-list .bl-right {
                float: right;
                margin-right: 50px;
            }

        .tab .t-item {
            font-size: 12px;
        }

        .content-body {
            display: flex;
            flex-direction: column;
            flex: 3;
        }

            .content-body .content-item {
            }

        .part-item {
            display: flex;
            align-items: center;
        }

        .part-tag {
            display: flex;
            flex-direction: column;
            background-color: #107ca2;
            opacity: 0.8;
            flex: 1;
            border-radius: 10px;
        }

            .part-tag + .part-tag {
                margin-left: 20px;
            }

        .item-top {
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 20px;
            color: #fff;
            border-bottom: 1px solid #f2f2f2;
            padding: 10px 0px;
        }

        .unit {
            display: flex;
            justify-content: flex-end;
            text-align: right;
            font-size: 16px;
            padding-right: 30px;
        }

        .num {
            display: flex;
            justify-content: center;
            flex: 1;
        }

        .unit-btn {
            display: flex;
            justify-content: center;
            padding: 10px 0px;
            font-size: 16px;
            color: #fff;
        }

        .do-btn {
            display: flex;
            justify-content: center;
            background-color: #107ca2;
            opacity: 0.8;
            flex: 1;
            border-radius: 10px;
            padding: 20px 0px;
            color: #fff;
        }
        .base-wrap{
           padding:15px 10px;
            height:100%;
        }
         .base-wrap .base-tit{
             font-size:12px;
             color:#fff;
        }
        .base-wrap .base-txt-wrap{
            margin-top:5px;
            background-color:#0B508B;
            border-radius:5px;
            color:#FFAE72;
            height:100%;
            align-items:center;
            justify-content:space-around;
        }
        .base-wrap .base-txt-wrap .num-1{
            background-color:#2A759C;
            padding:5px;
            font-size:40px;
        }
        .yj-info-1{
            height:100%;
        }
        .yj-info-1-list{
            color:#fff;
        }
        .yj-info-1 .telbg{
            height:100%;
            width:85px;
            background: url(../res/index/images/tel.png) center center no-repeat;
            background-size: contain;
        }
        .yj-info-1-list-item{
            padding:0 5px;
            background-color:#2A759C;
            margin-bottom:5px;
            font-size:12px;
            align-items:center;
            justify-content:center;
        }
         .yj-info-1-list-item:last-child{
              margin-bottom:0;
         }
         .yj-info-1-list-item .telnum{
             text-align:center;
             align-items:center;
             justify-content:center;
             display:flex;
         }
        .yj-info-1-info{
            color:#fff;
            background-color:#2A759C;
            text-align:center;
            font-size:16px;
            height:40px;
            line-height:40px;
            margin-top:5px;
        }
    </style>
</head>
<body>
    <div class="wrap flex flexV">
        <div class="bottom-wrap flex1">
            <div class="top">
                <div class="item flex1">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content pd0">
                                <div class="base-wrap flex flexV">
                                     <div class="base-tit">当前现场总人数</div>
                                     <div class="base-txt-wrap  flex flex1">
                                          <div class="num-1 specialNum" runat="server"  id="divperson">0</div>
                                        <div class="num-1 specialNum" runat="server"  id="person00">0</div>
                                        <div class="num-1 specialNum" runat="server"  id="person01">0</div>
                                        <div class="num-1 specialNum" runat="server"  id="person02">0</div>
                                     </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex3 itemlr">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='two' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex1">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div class="yj-info-1 flex flexV">
                                    <div class="flex flex1">
                                        <div style="height:100%;">
                                            <div class="telbg"></div> 
                                        </div>
                                        <div class="flex1 flex flexV yj-info-1-list">
                                            <div class="flex flex1 yj-info-1-list-item">
                                                <div class="flex1 telnum">110</div>
                                                <div>报警</div>
                                            </div>
                                            <div class="flex flex1 yj-info-1-list-item">
                                                <div class="flex1 telnum">119</div>
                                                <div>消防</div>
                                            </div>
                                            <div class="flex flex1 yj-info-1-list-item">
                                                <div class="flex1 telnum">120</div>
                                                <div>急救</div>
                                            </div>
                                            <div class="flex flex1 yj-info-1-list-item">
                                                <div class="flex1 telnum"></div>
                                                <div>项目部</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="yj-info-1-info">                                      
                                        <div>应 急 信 息</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap flex1">
            <div class="top">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up">
                        <div class="tab-wrap">
                            <div class="tab" data-value="4">
                                <div class="t-item active">施工分包商</div>
                                <div class="spline"></div>
                                <div class="t-item">问题类别</div>
                            </div>
                        </div>
                        <div class="bw-item-content">
                            <div id='four' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap flex1">
            <div class="top">
                <div class="item flex1">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='three' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex2 itemlr">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='five' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex1">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='six' style="width: 100%; height: 100%;"></div>
                            </div>
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
<script type="text/javascript">
    function category_Two(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: 'center',
                text: '项目安全人工时',
                textStyle: {
                    color: '#fff',
                    fontSize: 12,
                    fontWeight: '300'
                },
                show: true
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
                    interval: 0,
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
                color: 'rgba(9,199,113, 1)'
            },
            backgroundColor: 'rgba(0,162,233, 0.01)',
            textStyle: {
                color: 'rgba(255, 255, 255, 0.3)'
            }
        }
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }

    var two =<%=Two %>;
    var xArr = two.categories
    var data = [{
        name: '累计人工时',
        type: 'line',
        data: two.series[0].data,
        itemStyle: { normal: { color: '#00c771' } }
    },{
        name: '当月人工时',
            type: 'bar',
          barWidth: 40,
        data: two.series[1].data
    }]
    category_Two('two', xArr, data)
</script>
<script type="text/javascript">
    function category_Three(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: 'center',
                text: '作业许可数量统计',
                textStyle: {
                    color: '#fff',
                    fontSize: 12,
                    fontWeight: '300'
                },
                show: true
            },
            tooltip: {},
            legend: {
                show: false
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
                        color: 'rgba(255, 255, 255, 0.8)',
                         fontSize: 10,
                    }
                },
                type: 'category',
                data: xArr
            },
            xAxis: {
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

    var three =<%=Three %>;
    var xArr = three.categories
    var data = [{
        name: '作业许可数量统计',
        type: 'bar',
        data: three.series[0].data,
        itemStyle: { normal: { color: '#7BB259' } }
    }]
    category_Three('three', xArr, data)
</script>
<script type="text/javascript">
    function category_Four(id, xArr, series) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '安全检查问题统计',
                textStyle: {
                    color: '#fff',
                    fontSize: 12,
                    fontWeight: '300',
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
                    interval: 0,
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
                top: '25%',
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
    var four1 =<%=Four1 %>;
    var xArr = four1.categories
    var series = [{
        name: '待整改',
        type: 'bar',
         barWidth: 20,
         barGap:0.05,
        data: four1.series[0].data,
        itemStyle: { normal: { color: '#88cc00' } }
    }, {
        name: '全部',
            type: 'bar',    
         barWidth: 20,
        data: four1.series[1].data,
        itemStyle: { normal: { color: '#AE4B23' } }
    }];
    category_Four('four', xArr, series)
</script>
<script type="text/javascript">
    function category_Five(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: 'center',
                text: '入场安全培训',
                textStyle: {
                    color: '#fff',
                    fontSize: 12,
                    fontWeight: '300'
                },
                show: true
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
                    interval: 0,
                    rotate: 15,
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
    var five =<%=Five %>;
    var xArr = five.categories
    var data = [{
        name: '总数',
        type: 'line',
        smooth: true,
        data: five.series[1].data,
        itemStyle: { normal: { color: 'rgba(0, 255, 0, 1)' } }
    },{
        name: '当月',
           type: 'bar',
          barWidth: 40,
        data: five.series[0].data
    }]

    category_Five('five', xArr, data)
</script>
<script type="text/javascript">
    function category_six(id, xArr, data,data2) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: [{
                text: '事故统计',
                top: '0',
                left: '12%',
                textStyle: {
                    color: '#fff',
                    fontSize: 12,
                    fontWeight: 300
                }
            }],
            tooltip: {
                trigger: 'item',
                formatter: '{a} <br/>{b}: {c} ({d}%)'
            },
            legend: {
                orient: 'vertical',
                left: 'right',
                top: 'center',
                align: 'left',                
                data: ['人身伤害', '未遂事故', '火灾', '机械设备',  '环境影响', '其他'],
                textStyle: {//图例文字的样式
                    color: '#f2f2f2',
                      fontSize: 10,
                }
            },
            graphic: {
                type: "text",
                left: "23%",
                top: "55%",
                style: {
                    text: data2,
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 18,
                    fontWeight: 600
                }
            },
            color: ['#CFE5B7', '#BBD4A4', '#A3C78A', '#88B53D', '#7CA63B', '#698E30'],
            series: [
                {
                    name: '事故统计',
                    type: 'pie',
                    center: ['25%', '60%'],
                    radius: ['40%', '78%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false,
                        position: 'inside',
                        formatter: function (data) { return data.percent.toFixed(2); }
                    },
                    labelLine: {
                        show: false
                    },
                    data: [
                        { value: data[0], name: '人身伤害' },
                        { value:  data[1], name: '未遂事故' },
                        { value:  data[2], name: '火灾' },
                        { value:  data[3], name: '机械设备' },
                        { value:  data[4], name: '环境影响' },
                        { value:  data[5], name: '其他' }
                    ],
                    itemStyle: {
                        normal: {
                            borderWidth: 3,
                            borderColor: 'rgba(218,235,234, 1)'
                        }
                    }
                }
            ]
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
    var six=<%=Six %>;
    var xArr = ["分包一", "分包二"]
    var data = six.series[0].data
    var data2 = data[0]+data[1]+data[2]+data[3]+data[4]+data[5]
    category_six('six', xArr, data,data2)
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

        var four1 =<%=Four1 %>;
        var four2 =<%=Four2 %>;
        if (value == 4) {
            var xArr = four1.categories
            var data = [{
                name: '待整改',
                type: 'bar',
                data: four1.series[0].data
            }, {
                name: '全部',
                    type: 'bar',
                data: four1.series[1].data,
                itemStyle: { normal: { color: 'rgba(174,75,37, 1)' } }
            }];
            if (index == 2) {
                var xArr = four2.categories
                var data = [{
                    name: '待整改',
                    data: four2.series[0].data
                }, {
                    name: '全部',
                    data: four2.series[0].data,
                    itemStyle: { normal: { color: 'rgba(174,75,37, 1)' } }
                }];
            }
            category_Four('four', xArr, data)
        }
    })
</script>
</html>

