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
        .top {
            display: flex;
            display: -webkit-flex;
            overflow: hidden;
            width: 100%;
            height: 280px;
            margin-bottom: 5px;
            box-sizing: border-box;
        }

            .top .item {
                flex: 1;
                width: 50%;
                float: left;
                box-sizing: border-box;
                margin: 0 10px 20px;
            }

        .bw-b {
            width: 50%;
        }

        .bw-b-bottom-up {
            box-sizing: border-box;
            height: 260px;
        }

        .tab-wrap {
            left: auto;
            right: 15px;
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
            flex:2;
            margin:6px 10px;
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
    <div class="wrap">
        <div class="bottom-wrap">
            <div class="top">
                <div class="item">
                    <div class="bw-b-bottom ptop6">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='one' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content-body">
                    <div class="content-item">
                        <div class="part-item">
                            <div class="part-tag">
                                <div class="item-top">
                                    <div class="num">14822</div>
                                    <div class="unit">小时:</div>
                                </div>
                                <div class="unit-btn">项目安全工时</div>
                            </div>
                            <div class="part-tag">
                                <div class="item-top">
                                    <div class="num">239</div>
                                    <div class="unit">人:</div>
                                </div>
                                <div class="unit-btn">现场总人数</div>
                            </div>
                        </div>
                    </div>
                    <div class="content-item" style="margin-top:10px">
                        <div class="part-item">
                            <div class="part-tag">
                                <div class="item-top">
                                    <div class="num">632</div>
                                    <div class="unit">人/次:</div>
                                </div>
                                <div class="unit-btn">入场安全培训人次</div>
                            </div>
                            <div class="part-tag">
                                <div class="item-top">
                                    <div class="num">62</div>
                                    <div class="unit">个:</div>
                                </div>
                                <div class="unit-btn">作业许可数量（作业中）</div>
                            </div>
                        </div>
                    </div>
                    <div style="margin-top:10px;flex:1">
                        <div class="do-btn">应急信息管理</div>
                    </div>
                </div>
                <div class="item">
                    <div class="bw-b-bottom ptop6">
                        <div class="bw-b-bottom-up">
                            <div class="bw-item-content">
                                <div id='two' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap">
            <div class="top">
                <div class="item">
                    <div class="bw-b-bottom ptop6">
                        <div class="bw-b-bottom-up">
                            <div class="tab-wrap">
                                <div class="tab" data-value="4">
                                    <div class="t-item active">按单位</div>
                                    <div class="spline"></div>
                                    <div class="t-item">按类别</div>
                                </div>
                            </div>
                            <div class="bw-item-content">
                                <div id='four' style="width: 100%; height: 100%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom-wrap">
            <div class="bw-b-bottom-up">
                <div class="tab" data-value="6" style="border:0">
                    <div class="t-item active">人员信息</div>
                    <div class="spline"></div>
                    <div class="t-item ">机具信息</div>
                    <div class="spline"></div>
                    <div class="t-item">危大工程</div>
                    <div class="spline"></div>
                    <div class="t-item">...</div>
                </div>
                <div class="bw-item-content">
                                <div id='three' style="width: 100%; height: 100%;"></div>
                            </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript" src="../res/index/js/jquery-3.4.1.min.js"></script>
<script type="text/javascript" src="../res/index/js/swiper-3.4.2.jquery.min.js"></script>
<script type="text/javascript" src="../res/index/js/echarts.min.js"></script>
<script type="text/javascript">
    function category_One(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
                title: [{
	            text: '隐患整改单',
	            top:'0',
	            left:'center',
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
                orient: 'horizontal',
                left: 'center',
                top: '10%',
                data: ['已整改', '未整改'],
                textStyle:{//图例文字的样式
                    color:'#f2f2f2'
                }
            },
            color: ['#ff9933','#ff471a'],
            series: [
                {
                    name: '隐患整改单',
                    type: 'pie',
                    center:['50%','60%'],
                    radius: ['40%', '65%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false,
                        position: 'center'
                    },
                    emphasis: {
                        label: {
                            show: true,
                            fontSize: '20',
                            fontWeight: 'bold'
                        }
                    },
                    labelLine: {
                        show: false
                    },
                    data: [
                        {value: 335, name: '已整改'},
                        {value: 310, name: '未整改'}
                    ]
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
    category_One('one', xArr, data)
</script>
<script type="text/javascript">
    function category_Two(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        // 指定图表的配置项和数据
        var option = {
                title: [{
	            text: '事故统计',
	            top:'0',
	            left:'center',
	            textStyle:{
	                color: '#fff',
                    fontSize: 14,
                    fontWeight:300
	            }
	        }],
            tooltip: {
                trigger: 'item',
                formatter: '{a} <br/>{b}: {c} ({d}%)'
            },
            legend: {
                orient: 'horizontal',
                left: 'center',
                top: '10%',
                data: ['事故', '未遂事故'],
                textStyle:{//图例文字的样式
                    color:'#f2f2f2'
                }
            },
            color: ['#1bb2d8','#00ff80'],
            series: [
                {
                    name: '安全事故',
                    type: 'pie',
                    center:['50%','60%'],
                    radius: ['40%', '65%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false,
                        position: 'center'
                    },
                    emphasis: {
                        label: {
                            show: true,
                            fontSize: '20',
                            fontWeight: 'bold'
                        }
                    },
                    labelLine: {
                        show: false
                    },
                    data: [
                        {value: 0, name: '事故'},
                        {value: 0, name: '未遂事故'}
                    ]
                }
            ]
        }

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option)
    }
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
    category_Two('two', xArr, data)
</script>
<script type="text/javascript">
    function category_Four(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                // left:'center',
                text: '安全问题统计',
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
                name: '数量',
                type: 'bar',
                data: data
            }],
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
    var xArr = ["主项1", "主项2", "主项3", "主项4", "主项5", "主项6"]
    var data = [12, 5, 28, 43, 22, 11]
    category_Four('four', xArr, data)
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
           category_Four('four', xArr, data)
        }
    })
</script>

</html>

