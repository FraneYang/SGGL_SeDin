﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainMenu_CQMS.aspx.cs" Inherits="FineUIPro.Web.mainMenu_CQMS" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <link href="../res/index/css/reset.css" rel="stylesheet" />
    <link href="../res/index/css/home.css" rel="stylesheet" />
    <link href="../res/index/css/swiper-3.4.2.min.css" rel="stylesheet" />
    <style type="text/css">
         *{
            box-sizing:border-box;
        }
        .flexV{
            flex-direction:column;
        }
        .wrap{
            height:100%;
            padding:15px;
        }
        .bottom-wrap{
           padding:0;
        }
        .bw-b-bottom{
            width:100%;
            height:100%;
        }
        .bw-b-bottom-up {
            border-radius:0;
            height: 100%;
            margin:0;
        }
         .bw-item-content{
           padding:5px;
        }
        .top {
            width: 100%;
        }

        .top .item {
        }
        .bw-b {
            width: 50%;
        }

        .bw-b-bottom-up {
            
        }
         .tab-wrap {
            left: auto;
            right: 15px;
            top: 5px;
            font-size:12px;
        }

        .tab .t-item {
            width: auto;
            padding: 5px 10px;
        }
        .tit-item{
            padding: 0 10px;
            color:#fff;
            justify-content: space-between;
        }
        .tip-item{
            margin-left: 10px;
            align-items:center;
            font-size:10px;
        }
        .tip{
            width:25px;
            height:13px;
            background-color: #258F76;
            border-radius: 2px;
            margin-right:5px;
        }
        .tip-next {
            background-color: #92BF55;
        }
    </style>
</head>
<body>
    <div class="wrap flex flexV">
        <div class="bottom-wrap flex1">
            <div class="top flex">
                <div class="item flex1 flex flexV">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content flex">
                                <div id='one1' class="flex1"  style="width: 100%; height: 100%;"></div>
                                <div id='one2' class="flex1" style="width: 100%; height: 100%;"></div>
                                <div id='one3' class="flex1" style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content flex flexV" style="padding:0;position:relative;">
                                <div class="flex " style="position:absolute;width:100%;font-size:14px;">
                                    <div class="flex flex1  tit-item">
                                        <div class="tit">质量控制点通知</div>
                                        <div class="flex">
                                            <div class="tip-item flex">
                                                <div class="tip"></div>
                                                <div>已完成</div>
                                            </div>
                                            <div class="tip-item flex">
                                                <div class="tip tip-next"></div>
                                                <div>未完成</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="flex flex1">
                                    <div id='one4' class="flex1" style="width: 100%; height: 100%;"></div>
                                    <div id='one5' class="flex1" style="width: 100%; height: 100%;"></div>
                                    <div id='one6' class="flex1" style="width: 100%; height: 100%;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex2">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="2">
                                    <div class="t-item active">施工分包商</div>
                                    <div class="spline"></div>
                                    <div class="t-item ">单位工程</div>
                                    <div class="spline"></div>
                                    <div class="t-item">专业</div>
                                </div>
                            </div>
                            <div class="bw-item-content">
                                <div id='two' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap flex1">
            <div class="top flex">
                <div class="item flex1">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="3">
                                    <div class="t-item active">施工分包商</div>
                                    <div class="spline"></div>
                                    <div class="t-item">专业</div>
                                </div>
                            </div>
                            <div class="bw-item-content">
                                <div id='three' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex2">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='four' style="width: 100%; height: 100%;"></div>
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
    function category_One(id, title, dataNum) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            //tooltip: {
            //    formatter: '{a} <br/>{b} : {c}%'
            //},
            title: {
                left: 'center',
                bottom: '0',
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
                    radius: "100%",
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
        category_One('one1', "项目质量验收一次合格率", 80)
        category_One('one2', "项目施工资料同步率", 40)
        category_One('one3', "项目质量问题整改完成率", 20)
</script>
    <script type="text/javascript">
    function category_six(id, title, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: '0',
                top: '0',
                text: '统计',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
                },
                show: true
            },
            legend: {
                left: 'right',
                textStyle: {//图例文字的样式
                    color: '#ffffff'
                }
            },
            tooltip: {},
            dataset: {
                source: [
                    ['product', '2012', '2013', '2014'],
                    ['已整改', 2, 3, 5],
                    ['未整改', 6, 9, 3]
                ]
            },
            color: ['#258F76', '#92BF55'],
            graphic: {
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "text",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 18,
                    fontWeight: 700
                }
            },
            series: [{
                type: 'pie',
                center: ['18%', '58%'],
                radius: ['60%', '80%'],
                avoidLabelOverlap: false,
                label: {
                    show: true,
                    position: 'inside',
                    formatter: function(data){ return data.percent.toFixed(2)+"%";} 
                },
                itemStyle: {
                    normal: {
                        //opacity: 0.7,
                        borderWidth: 3,
                        borderColor: 'rgba(218,235,234, 1)'
                    }
                },
                // No encode specified, by default, it is '2012'.
            }, {
                type: 'pie',
                radius: ['60%', '80%'],
                center: ['50%', '58%'],
                avoidLabelOverlap: false,
                label: {
                    show: true,
                    position: 'inside',
                    formatter: function(data){ return data.percent.toFixed(2)+"%";} 
                    },
                itemStyle: {
                    normal: {
                        //opacity: 0.7,
                        borderWidth: 3,
                        borderColor: 'rgba(218,235,234, 1)'
                    }
                },
                encode: {
                    itemName: 'product',
                    value: '2013'
                }
            }, {
                type: 'pie',
                radius: ['60%', '80%'],
                center: ['82%', '58%'],
                avoidLabelOverlap: false,
                label: {
                    show: true,
                    position: 'inside',
                    formatter: function(data){ return data.percent.toFixed(2)+"%";} 
                    },
                itemStyle: {
                    normal: {
                        //opacity: 0.7,
                        borderWidth: 3,
                        borderColor: 'rgba(218,235,234, 1)'
                    }
                },
                encode: {
                    itemName: 'product',
                    value: '2014'
                }
            }]
        };

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
        var xArr = ["已整改", "未整改"]
        var series = [
            { value: 335, name: '人身伤害' },
            { value: 310, name: '未' }
        ]
        //category_six('one4', "A类", xArr, );
</script>
<script type="text/javascript">
    function pie(id, data, title, text, needLegend) {
        var myChartPie = echarts.init(document.getElementById(id));
        var needLegend = needLegend || false
        var optionPie = {
            tooltip: {
                trigger: 'item',
                show: false
            },
            legend: {
                show: needLegend,
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
                    fontSize: 14,
                    fontWeight:'300'
                },
                show: true
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
            color: ['#258F76', '#92BF55'],
            series: [
                {
                    name: ' ',
                    hoverOffset: 0,
                    type: 'pie',
                    clickable:false,
                    radius: ['50%', '65%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false
                    },
                    itemStyle: {
                        normal: {
                            //opacity: 0.7,
                            borderWidth: 3,
                            borderColor: 'rgba(218,235,234, 1)'
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
    var data = [{ value: 10, name: '未整改' },
    { value: 5, name: '已整改' }];
    pie('one4', data, "A类", "15")
    pie('one5', data, "B类", "23")
    pie('one6', data, "C类", "45")
</script>

<script type="text/javascript">
    function category_Two(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '质量问题统计',
                textStyle: {
                    color: '#fff',
                    fontWeight: 'normal',
                    fontSize:16
                },
                show: true
            },
            tooltip: {},
            legend: {
                left: '15%',
                show: true,
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
                data: xArr,
                boundaryGap: [0, 0.01],
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
        myChart.setOption(option)
    }
    var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6", "分包7", "分包8", "分包9"]
    var data = [
        {
            name: '未整改',
            type: 'bar',
            stack: '总量',
            data: [3, 5, 8, 10, 6, 4, 5, 9, 12],
            itemStyle: { normal: {  color: 'rgba(162,63,21, 1)' } }
        },
        {
            name: '已整改',
            type: 'bar',
            stack: '总量',
            data: [2, 7, 5, 9 ,12, 9, 2, 8, 10],
            itemStyle: { normal: { color: 'rgba(206,143,135,1)' } }
        }
    ]
    category_Two('two', xArr, data)
</script>
<script type="text/javascript">
    function category_Three(id, xArr, series)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'center',
                text: '作业许可数量统计',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
                },
                show: false
            },
            tooltip: {},
            legend: {
                left: '3%',
                orient: 'vertical',
                top: '0',
                show: true,
                selectedMode: false,
                textStyle:{//图例文字的样式
                    color: '#ffffff',
                    fontSize: '12'
                }
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
            series: series,
            grid: {
                top: '20%',
                left: '10',
                right: '10',
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
        myChart.setOption(option)
    }
    var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6"]
    var data = [12, 5, 28, 43, 22, 11]
    var data1 = [21, 9, 12, 15, 8, 43]
    var series = [{
        name: '质量一次性合格率',
        type: 'bar',
        data: data,
        itemStyle: { normal: { color: 'rgba(43,155,176,1)' } }
    },
    {
        name: '施工资料同步率',
        type: 'bar',
        data: data1,
        itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
    }]
    category_Three('three', xArr, series)
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
                show: true,
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
    },
    {
        name: '施工资料同步率',
        type: 'bar',
        data: data1,
        itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
    }];
    category('four', xArr, series)
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

        if (value == 2) {
            var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6", "分包7", "分包8", "分包9"]
            var data = [
                {
                    name: '未整改',
                    type: 'bar',
                    stack: '总量',
                    data: [3, 5, 8, 10, 6, 4, 5, 9, 12],
                    itemStyle: { normal: { color: 'rgba(162,63,21, 1)' } }
                },
                {
                    name: '已整改',
                    type: 'bar',
                    stack: '总量',
                    data: [2, 7, 5, 9, 12, 9, 2, 8, 10],
                    itemStyle: { normal: { color: 'rgba(206,143,135,1)' } }
                }
            ];
            if (index == 2) {
                xArr =["单位工程1", "单位工程2", "单位工程3", "单位工程4", "单位工程5", "单位工程6", "单位工程7", "单位工程8", "单位工程9"]
                data = [
                {
                    name: '未整改',
                    type: 'bar',
                    stack: '总量',
                    data: [13, 5, 18, 10, 6, 4, 5, 9, 22],
                    itemStyle: { normal: { color: 'rgba(162,63,21, 1)' } }
                },
                {
                    name: '已整改',
                    type: 'bar',
                    stack: '总量',
                    data: [2, 7, 15, 9, 12, 29, 12, 18, 10],
                    itemStyle: { normal: { color: 'rgba(206,143,135,1)' } }
                }
            ];
            }
            else if (index == 4) {
                xArr = ["专业1", "专业2", "专业3", "专业4", "专业5", "专业6", "专业7", "专业8", "专业9"]
                data = [
                {
                    name: '未整改',
                    type: 'bar',
                    stack: '总量',
                    data: [23, 25, 18, 10, 16, 4, 5, 29, 2],
                    itemStyle: { normal: { color: 'rgba(162,63,21, 1)' } }
                },
                {
                    name: '已整改',
                    type: 'bar',
                    stack: '总量',
                    data: [2, 27, 15, 9, 12, 29, 32, 18, 1],
                    itemStyle: { normal: { color: 'rgba(206,143,135,1)' } }
                }
            ];
            }
            category_Two('two', xArr, data)
        }
        else if (value == 3) {
            var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6"]
            var data = [12, 5, 28, 43, 22, 11]
            var data1 = [21, 9, 12, 15, 8, 43]
            var series = [{
                name: '质量一次性合格率',
                type: 'bar',
                data: data,
                itemStyle: { normal: { color: 'rgba(43,155,176,1)' } }
            },
            {
                name: '施工资料同步率',
                type: 'bar',
                data: data1,
                itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
            }];
            if (index == 2) {
                 xArr = ["专业1", "专业2", "专业3", "专业4", "专业5", "专业6"]
            }
             category_Three('three', xArr, series)
        }
    })
</script>

</html>

