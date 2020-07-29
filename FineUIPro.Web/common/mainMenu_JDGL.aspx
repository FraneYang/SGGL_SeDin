<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainMenu_JDGL.aspx.cs" Inherits="FineUIPro.Web.mainMenu_JDGL" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <link href="../res/index/css/reset.css" rel="stylesheet" />
    <link href="../res/index/css/home.css" rel="stylesheet" />
    <link href="../res/index/css/swiper-3.4.2.min.css" rel="stylesheet" />
    <style>
         .wrap{
            height:100%;
            padding:15px;
        }
         .top-wrap{
            height:100%;
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
        .bottom-wrap{
        padding:0;
        }
        .pdtb0{
            padding-top:0 !important;
            padding-bottom:0 !important;
        }
        .tit-center{
            text-align:center;
        }
        .tit-one{
            
        }
        .pdl{
            padding-left:15px;
        }
        .more{
            text-align:right;
            padding: 10px;
        }
        .bg-img{
            width:100%;
            max-height:225px;
        }  
        .bg-img-1{
           height:100%;
        width:100%;
        border-radius:5px;
        background: url(../res/images/SUBimages/SEDIN.gif) center center no-repeat;
        background-size: 100% 100%;
        }
    </style>
</head>
<body>
    <div class="wrap flex">
        <div class="flex3 flex flexV top-wrap">
            <div class="item flex1">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one tit-center">按单位统计周计划</div>
                        <div class="bw-item-content flex1 pd0 flex">
                            <div class="flex1" id='one1' style="width: 100%; height: 100%;"></div>
                            <div class="flex1" id='one2' style="width: 100%; height: 100%;"></div>
                            <div class="flex1" id='one3' style="width: 100%; height: 100%;"></div>
                            <div class="flex1" id='one4' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item flex1">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one tit-center">赢得值曲线</div>
                        <div class="bw-item-content flex1 pdtb0">
                            <div id='two' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item flex1">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one tit-center">施工进度统计</div>
                        <div class="bw-item-content flex1 pdtb0">
                            <div id='three' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="flex1 flex flexV">
            <div class="item-one flex1">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one pdl">进度统计</div>
                        <div class="bw-item-content flex1 pdtb0">
                             <div id='four' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item-two flex2">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one pdl">项目工程节点</div>
                        <div class="bw-item-content flex1 pdtb0">
                             <div id='five' style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item-three flex3">
                <div class="bw-b-bottom">
                    <div class="bw-b-bottom-up flex flexV">
                        <div class="tit-one pdl">形象进度照片</div>
                        <div class="bw-item-content flex1 pdtb0">
                            <div style="width:100%;height:100%;">
                                <div class="bg-img-1"></div>
                                <%--<img  src="../res/images/my_face_80.jpg" alt="Alternate Text"  class="bg-img"/>--%>
                            </div>
                        </div>
                        <div class="more">更多</div>
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
    line('two', xArr1, data1)
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
    },
    {
        name: '施工资料同步率',
        type: 'bar',
        data: data1,
        itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
    }];
    category('three', xArr, series)
    category('five', xArr, series)
</script>
<script type="text/javascript">
    function pie(id, title, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
                title: [{
	            text: title,
	            top:'0',
	            left:'20%',
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
                orient: 'vertical',
                left: 'right',
                top: 'center',
                align :'left',
                data: xArr,
                textStyle:{//图例文字的样式
                    color:'#f2f2f2'
                }
            },
            color: ['#1D9A78','#8BC145','#36AFCE','#1D6FA9'],
            series: [
                {
                    name: title,
                    type: 'pie',
                    center: ['33%', '50%'],
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: true,
                        textStyle:{
                            color:'#333'
                        },
                        position: 'inside',
                        formatter: function(data){ return data.percent.toFixed(0)+"%";} 
                    },
                    labelLine: {
                        show: false
                    },
                    data: data,
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
    var xArr = ['第一季度', '第二季度', '第三季度','第四季度']
    var data = [
       { value: 58, name: '第一季度' },
    { value: 23, name: '第二季度' },
    { value: 10, name: '第三季度' },
    { value: 9, name: '第四季度' }
    ]
    pie('one1', '周计划', xArr, data)
    pie('one2', '周计划', xArr, data)
    pie('one3', '周计划', xArr, data)
    pie('one4', '周计划', xArr, data)
</script>
<script>
    var dataX = ['项目', '工程']
    var data = [20, 60]
    var dataT = [100, 100]
    echartsBarInit('four', "进度统计", dataX, data, dataT);
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
                    show: false
                },
                grid: {   // 直角坐标系内绘图网格
                    left: '0',  //grid 组件离容器左侧的距离,
                    //left的值可以是80这样具体像素值，
                    //也可以是'80%'这样相对于容器高度的百分比
                    top: '10',
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
</html>

