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
        .bw-item-content{
           padding:5px;
        }
        .top {
            display: flex;
            display: -webkit-flex;
            overflow: hidden;
            width: 100%;
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
        .yj-info{
           align-items: center;
           justify-content: center;
           width:100%;
           height:100%;
           color:red;
           font-size:20px;
        }
        .yj-info .tit{
            font-weight:700;
            margin-bottom:20px;
        }
        .yj-info .tel{
            font-weight:700;
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
            display:flex;
            flex-direction:column;
            flex:3;
        }
        .content-body .content-item {

        }
        .part-item {
            display:flex;
            align-items:center;
        }
        .part-tag {
            display:flex;
            flex-direction:column;
            background-color:#107ca2;
            opacity: 0.8;
            flex:1;
            border-radius:10px;
        }
        .part-tag +.part-tag {
            margin-left:20px;
        }
        .item-top {
            display:flex;
            justify-content:center;
            align-items:center;
            font-size:20px;
            color:#fff;
            border-bottom:1px solid #f2f2f2;
            padding:10px 0px;
        }
        .unit {
            display:flex;
            justify-content:flex-end;
            text-align:right;
            font-size:16px;
            padding-right:30px;
        }
        .num {
            display:flex;
            justify-content:center;
            flex:1;
        }
        .unit-btn {
            display:flex;
            justify-content:center;
            padding:10px 0px;
            font-size:16px;
            color:#fff;
        }
        .do-btn {
            display:flex;
            justify-content:center;
            background-color:#107ca2;
            opacity: 0.8;
            flex:1;
            border-radius:10px;
            padding:20px 0px;
            color:#fff;
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
                            <div class="bw-item-content">
                                <div id='one' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex3">
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
                                <div class="yj-info flex flexV">
                                    <div class="tit">应急信息</div>
                                    <div class="tel">应急电话：123456</div>
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
                <div class="item flex2">
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
    function category_One(id, dataNum) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            //tooltip: {
            //    formatter: '{a} <br/>{b} : {c}%'
            //},
            title: {
                left:'center',
                text: '当前现场人数',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
                },
                show: true
            },
            series: [
                {
                    name: ' ',
                    center: ["50%", "65%"],
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
    category_One('one', 80)
</script>
<script type="text/javascript">
    function category_Two(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'center',
                text: '项目安全人工时',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
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
    var xArr = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"]
    var data = [ {
        name: '项目安全人工时',
        type: 'line',
        //smooth: true,
        data:  [3, 5, 2, 3, 4, 2, 9, 8, 4, 7, 6, 1]
    }]
    category_Two('two', xArr, data)
</script>
<script type="text/javascript">
    function category_Three(id, xArr, data)  {
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
    var xArr = ["待提交", "审核中", "作业中", "已关闭", "已取消", "作废"]
    var data = [ {
        name: '作业许可数量统计',
        type: 'bar',
        data: [13, 15, 23, 13, 22, 12],
        itemStyle: { normal: { color: 'rgba(135,191,90,.9)' } }
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
                    fontWeight:'300'
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
    var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6", "分包7", "分包8", "分包9"]
    var data = [12, 5, 28, 43, 22, 11, 23, 50, 8]
    var data1 = [23, 35, 12, 33, 20, 31, 40, 45, 24]
    var series = [{
        name: '数量',
        type: 'bar',
        data: data
    }, {
        name: '数量',
        type: 'bar',
        data: data1,
        itemStyle: { normal: { color: 'rgba(174,75,37, 1)' } }
    }];
    category_Four('four', xArr, series)
</script>
<script type="text/javascript">
    function category_Five(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'center',
                text: '人场安全培训',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight:'300'
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
    var data = [ {
        name: '',
        type: 'line',
        smooth: true,
        data: [23, 25, 22, 13, 4, 12, 9],
        itemStyle: { normal: { color: 'rgba(30,81,134, 1)' } }
    }, {
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
    function category_six(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
                title: [{
	            text: '事故统计',
	            top:'0',
	            left:'12%',
	            textStyle:{
	                color: '#fff',
                    fontSize: 16,
                    fontWeight:300
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
                align :'left',
                data: ['人身伤害', '未遂事故', '火灾','机械设备','重大','其他'],
                textStyle:{//图例文字的样式
                    color:'#f2f2f2'
                }
            },
            color: ['#CFE5B7','#BBD4A4','#A3C78A','#88B53D','#7CA63B','#698E30'],
            series: [
                {
                    name: '事故统计',
                    type: 'pie',
                    center: ['25%', '60%'],
                    radius: ['40%', '80%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: true,
                        position: 'inside',
                        formatter: function(data){ return data.percent.toFixed(2)+"%";} 
                    },
                    //emphasis: {
                    //    label: {
                    //        show: true,
                    //        fontSize: '20',
                    //        fontWeight: 'bold'
                    //    }
                    //},
                    labelLine: {
                        show: false
                    },
                    data: [
                        { value: 335, name: '人身伤害' },
                        { value: 310, name: '未' },
                        { value: 335, name: '火灾' },
                        { value: 310, name: '机械设备' },
                        { value: 335, name: '重大' },
                        { value: 310, name: '其他' }
                    ],
                    itemStyle: {
                        normal: {
                            //opacity: 0.7,
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
    var xArr = ["分包一", "分包二"]
    var data = [
        {
            name: '质量一次性合格率',
            type: 'bar',
            data: [0.85, 0.82],
            //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
        },
        {
            name: '施工资料同步率',
            type: 'bar',
            data: [0.69, 0.65],
            //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
        }
    ]
    category_six('six', xArr, data)
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

        if (value == 4) {
            var xArr = ["分包1", "分包2", "分包3", "分包4", "分包5", "分包6", "分包7", "分包8", "分包9"]
            var data = [12, 5, 28, 43, 22, 11, 23, 50, 8]
            var data1 = [23, 35, 12, 33, 20, 31, 40, 45, 24]
            var series = [{
                name: '数量',
                type: 'bar',
                data: data
            }, {
                name: '数量',
                type: 'bar',
                data: data1,
                itemStyle: { normal: { color: 'rgba(174,75,37, 1)' } }
            }];
            if (index == 2) {
                 var xArr = ["类别1", "类别2", "类别3", "类别4", "类别5", "类别6", "类别7", "类别8", "类别9"]
            var data = [12, 25, 28, 43, 22, 21, 23, 50, 28]
            var data1 = [23, 35, 12, 33, 20, 31, 40, 5, 14]
            var series = [{
                name: '数量',
                type: 'bar',
                data: data
            }, {
                name: '数量',
                type: 'bar',
                data: data1,
                itemStyle: { normal: { color: 'rgba(174,75,37, 1)' } }
            }];
            }
           category_Four('four', xArr, data)
        }
    })
</script>
</html>

