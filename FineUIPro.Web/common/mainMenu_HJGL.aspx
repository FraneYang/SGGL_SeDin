<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainMenu_HJGL.aspx.cs" Inherits="FineUIPro.Web.mainMenu_HJGL" %>

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
        .pd{
            padding: 20px;
        }
        .pd10{
            padding: 10px !important;
        }
        .wrap{
            height:100%;
            padding:15px;
        }
        .bw-item-content{
            padding: 0;
        }
        .bottom-wrap{
           padding:0;
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

        .tab-wrap {
            left: auto;
            right: 15px;
            top: 10px;
        }
        .tab{
            border: none;
            box-shadow: none;
        }
        .tab .active {
            color: #fff;
            background-color: transparent;
        }
        .tab .t-item {
            width: auto;
            font-size: 13px;
            padding: 2px;
        }

    </style>
</head>
<body>
    <div class="wrap flex flexV">
        <div class="bottom-wrap flex1">
            <div class="top">
                <div class="item flex3">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content flex">
                                <div class="flex1 pd" >
                                     <div id='one'  style="width: 100%; height: 100%;"></div>
                                </div>
                               <div class="flex1 pd" >
                                     <div id='one1'   style="width: 100%; height: 100%;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex2">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='two' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex3">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="2">
                                    <div class="t-item active">按分包商</div>
                                    <div class="t-item ">按单位工程</div>
                                    <div class="t-item">按材质类别</div>
                                </div>
                            </div>
                            <div class="bw-item-content pd10">
                                <div id='three' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap flex1">
            <div class="top">
                <div class="item flex3">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="3">
                                    <div class="t-item active">按分包商</div>
                                    <div class="t-item ">按单位工程</div>
                                    <div class="t-item">按材质类别</div>
                                </div>
                            </div>
                            <div class="bw-item-content pd10">
                                <div id='four' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex2">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="4">
                                    <div class="t-item active">按分包商</div>
                                    <div class="t-item ">按单位工程</div>
                                    <div class="t-item">按材质类别</div>
                                </div>
                            </div>
                            <div class="bw-item-content">
                                <div id='five' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item flex3">
                    <div class="bw-b-bottom">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="3">
                                    <div class="t-item active">按分包商</div>
                                    <div class="t-item ">按单位工程</div>
                                    <div class="t-item">按材质类别</div>
                                </div>
                            </div>
                            <div class="bw-item-content pd10">
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
    function category_One(id, dataNum, title, detail) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            //tooltip: {
            //    formatter: '{a} <br/>{b} : {c}%'
            //},
            title: {
                left:'center',
                text: title,
                textStyle: {
                    color: '#278AC8',
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
                        show: true,
                        formatter: detail
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
    category_One('one', 80, '项目焊接一次合格率', '{value}%');
    category_One('one1', 76, '项目焊工总人数', '{value}人');
</script>
<script type="text/javascript">
    function category_two(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
                title: [{
	            text: '项目焊接工程量统计',
	            top:'7%',
	            left:'center',
	            textStyle:{
	                color: '#278AC8',
                    fontSize: 14,
                    fontWeight:300
	            }
	        }],
            //tooltip: {
            //    trigger: 'item',
            //    formatter: '{a} <br/>{b}: {c} ({d}%)'
            //},
            legend: {
                show: true,
                bottom: 5,
                align: 'left',
                textStyle:{//图例文字的样式
                    color:'#fff'
                }
            },
            color: ['#1E9B7D', '#8EC43D'],
            series: [
                {
                    name: '项目焊接工程量统计',
                    type: 'pie',
                    center: ['50%', '55%'],
                    radius: ['50%', '65%'],
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
                        { value: 35, name: '已完成' },
                        { value: 10, name: '未完成' }
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
    category_two('two', xArr, data)
</script>
<script type="text/javascript">
    function category_Three(id, xArr, series, title) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: title,
                textStyle: {
                    fontSize: 14,
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
    var xArr = ["类别1", "类别2", "类别3", "类别4", "类别5", "类别6", "类别7", "类别8", "类别9"]
    var data = [12, 5, 28, 43, 22, 11, 23, 50, 8]
    var series = [{
        name: '数量',
        type: 'bar',
        data: data,
        itemStyle: { normal: { color: 'rgba(236,157,27, 1)' } }
    }];
   
    category_Three('three', xArr, series, '安全检查问题统计')

     var series1 = [{
        name: '数量',
        type: 'bar',
        data: data,
        itemStyle: { normal: { color: 'rgba(57,178,210, 1)' } }
    }];
    category_Three('four', xArr, series1, '焊接一次合格率')
</script>
<script type="text/javascript">
    function category_five(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例 
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                top: 10,
                left: 10,
                text: '焊接缺陷分析',
                textStyle: {
                    color: '#fff',
                    fontWeight: '300',
                    fontSize: 14
                },
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                show: false,
                left: 'center',
                data: ['降水量', '蒸发量']
            },
            radar: [
                {
                    shape: 'circle',
                    indicator: (function () {
                        var res = [];
                        for (var i = 1; i <= 12; i++) {
                            res.push({ text: '问题' + i , max: 100 });
                        }
                        return res;
                    })(),
                    center: ['50%', '55%'],
                    radius: 90
                }
            ],
            series: [
                {
                    type: 'radar',
                    //areaStyle: {},
                    data: [
                        {
                            name: '降水量',
                            value: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 75.6, 82.2, 48.7, 18.8, 6.0, 2.3],
                        },
                        {
                            name: '蒸发量',
                            value: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 35.6, 62.2, 32.6, 20.0, 6.4, 3.3]
                        }
                    ]
                }
            ]
        };


        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
    var xArr = ["类别1", "类别2", "类别3", "类别4", "类别5", "类别6", "类别7"]
    var data = [ {
        name: '',
        type: 'line',
        data: [23, 25, 22, 13, 4, 12, 9],
        itemStyle: { normal: { color: 'rgba(110,164,133, 1)' } }
    }, {
            name: '',
            type: 'bar',
            data: [20, 2, 1, 34, 39, 30, 20],
            itemStyle: { normal: { color: 'rgba(160,181,204, 1)' } }
        }]
    category_five('five', xArr, data)
</script>
<script type="text/javascript">
    function category_six(id, xArr, data)  {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left:'10',
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
        data: [23, 25, 22, 13, 4, 12, 9],
        itemStyle: { normal: { color: 'rgba(110,164,133, 1)' } }
    }, {
            name: '',
            type: 'bar',
            data: [20, 2, 1, 34, 39, 30, 20],
            itemStyle: { normal: { color: 'rgba(160,181,204, 1)' } }
        }]
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

        if (value == 1) {

            var xArr = ["单位工程一", "单位工程二"]
            var data = [
                {
                    name: '质量一次性合格率',
                    type: 'bar',
                    data: [0.9, 0.85],
                    //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                },
                {
                    name: '施工资料同步率',
                    type: 'bar',
                    data: [0.75, 0.65],
                    //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                }
            ]
            if (index == 0) {
                xArr = ["分包一", "分包二"]
                data = [
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
            } else if (index == 4) {
                xArr = ["建筑", "安装"]
                data = [
                    {
                        name: '质量一次性合格率',
                        type: 'bar',
                        data: [0.83, 0.78],
                        //itemStyle: { normal: { color: 'rgba(231,236,114,.9)' } }
                    },
                    {
                        name: '施工资料同步率',
                        type: 'bar',
                        data: [0.64, 0.61],
                        //itemStyle: { normal: {  color: 'rgba(200,201,10, 1)' } }
                    }
                ]
            }
            category_One('one', xArr, data)
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
            var data = [{ value: 10, name: '分包一' },
            { value: 5, name: '分包二' },
            { value: 15, name: '分包三' },
            { value: 25, name: '分包四' },
            { value: 20, name: '分包五' },
            { value: 35, name: '分包六' }];
            if (index == 2) {
                data = [{ value: 25, name: '单位工程一' },
                { value: 35, name: '单位工程二' },
                { value: 30, name: '单位工程三' }];
            }
            else if (index == 4) {
                data = [{ value: 45, name: '建筑' },
                { value: 55, name: '安装' }];
            }
            else if (index == 6) {
                data = [{ value: 35, name: '质量不合格' },
                { value: 65, name: '质量缺陷' }];
            }
            pie('three', data)
        }
        else if (value == 4) {
            var xArr = ["主项一", "主项二", "主项三", "主项四", "主项五"]
            var data = [12, 5, 28, 43, 22]
            if (index == 2) {
                xArr = ["建筑", "安装"]
                data = [45, 65]
            }
            category('four', xArr, data)
        }
    })
</script>
</html>

