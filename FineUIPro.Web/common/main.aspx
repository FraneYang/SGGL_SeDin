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
        .row2 .item {
            height: 100%;
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

        .content-wrap {
            padding: 0 20px 20px;
        }

        @media screen and (max-height: 625px) {
            .swiperHeightWrap {
                height: 170px;
            }

            .swiperHeight {
                height: 90px;
            }

            .content-wrap {
                padding: 0 10px 10px;
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

        .link {
            color: #fff;
        }

        .map-desc {
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
        }

            .map-desc .desc {
                color: #00a2e9;
                /*text-shadow: 0 0 10px #7a7cd0,0 0 20px #7a7cd0,0 0 30px #7a7cd0,0 0 40px #7a7cd0;*/ /*设置发光效果*/
                text-shadow: 0 0 10px #00a2e9;
            }

                .map-desc .desc .d-item {
                    /*border-top: 1px solid #00a2e9;
            border-bottom: 1px solid #00a2e9;*/
                    padding: 3px 10px;
                    /*background-color: rgba(1, 30, 50 ,0.3);*/
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    margin-bottom: 5px;
                    position: relative;
                }

                    .map-desc .desc .d-item:last-child {
                        margin-bottom: 0;
                    }

                    .map-desc .desc .d-item:before, .map-desc .desc .d-item:after {
                        content: '';
                        display: inline-block;
                        width: 12px;
                        height: 12px;
                        position: absolute;
                    }

                    .map-desc .desc .d-item:before {
                        width: 100%;
                        top: 0;
                        left: 0;
                        border-top: 1px solid #00a2e9;
                        border-left: 1px solid #00a2e9;
                    }

                    .map-desc .desc .d-item:after {
                        width: 100%;
                        bottom: 0;
                        right: 0;
                        border-bottom: 1px solid #00a2e9;
                        border-right: 1px solid #00a2e9;
                    }

                .map-desc .desc .d-item-1 {
                    /*background-color: rgba(0,53,97 ,0.3) !important;*/
                }

                .map-desc .desc .d-item .tit {
                    font-size: 10px;
                }

                .map-desc .desc .d-item .num {
                    font-size: 18px;
                }

        .project-wrap {
            position: absolute;
            left: 50%;
            top: 0px;
            transform: translateX(-50%);
            color: #fff;
        }

            .project-wrap .project {
                border: none;
                position: relative;
                /*min-width: 100px;*/
            }

            .project-wrap .project-tit {
                padding: 5px 10px;
                color: #00a2e9;
                font-size: 12px;
                border: none;
                background-color: transparent;
                position: relative;
                /*min-width: 100px;*/
            }

        .project-tit-wrap {
            position: relative;
        }

        /*.project-wrap .project-tit-wrap:before {
            content: '';
            position: absolute;
            right: 8px;
            top: 50%;
            width: 8px;
            height: 8px;
            border-top: 1px solid #fff;
            border-right: 1px solid #fff;
            transform: translateY(-50%) rotate(135deg);
            z-index: 999;
        }

        .project-wrap:hover .project-tit-wrap:before {
            transform: translateY(-50%) rotate(-45deg);
        }*/

        .project-list {
            display: none;
            background-color: rgba(1,82,138, 0.8);
            color: #fff;
            font-size: 12px;
            color: #00a2e9;
        }

            .project-list > div {
                padding: 5px 10px;
                cursor: pointer;
            }

                .project-list > div:hover {
                    background-color: rgba(1,82,138, 0.9);
                    color: #00a2e9;
                }

        .tab-content .line-item {
            background-color: #0B5EA5;
            border-radius: 10px;
            height: 10px;
        }

            .tab-content .line-item > div {
                background-color: #29D8DD;
            }

        .Accumulation-next .tab-h .txt, .Accumulation-next .tab-i .txt {
            width: 45px;
        }

        .tab-content .txt {
            display: flex;
            align-items: center;
        }

        @media screen and (min-height:780px) {
            .tab-content .line-item {
                border-radius: 25px;
                height: 50px;
            }

                .tab-content .line-item > div {
                    border-radius: 25px;
                }
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
<body>
    <div class="wrap">
        <div class="bottom-wrap flex">
            <!--左侧-->
            <div class="bw-s flex1 flex flexV">
                <div class="bg-item flex1">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">安全数据统计</div>
                        <div class="content-wrap-1 flex flexV">
                            <div class="item flex1 flex flexV">
                                <div class="tit">安全人工时</div>
                                <div class="content-1 flex flex1">
                                    <div class="cc-num-wrap flex1 flex">
                                        <div class="specialNum cc-num" runat="server" id="divPNum8">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum7">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum6">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum5">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum4">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum3">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum2">0</div>
                                        <div class="specialNum cc-num" runat="server" id="divPNum1">0</div>
                                    </div>
                                    <%--  <div class="unit">小时</div>--%>
                                </div>

                            </div>
                            <div class="item flex1 flex flexV">
                                <div class="tit">安全隐患整改单</div>
                                <div class="content flex flex1">
                                    <div class="t-item">
                                        <div class="specialNum c-num" runat="server" id="divAllRectify">0</div>
                                        <div class="c-txt">总数（个）</div>
                                    </div>
                                    <div class="t-item">
                                        <div class="specialNum c-num" runat="server" id="divCRectify">0</div>
                                        <div class="c-txt">已完成（个）</div>
                                    </div>
                                    <div class="t-item">
                                        <div class="specialNum c-num" runat="server" id="divUCRectify">0</div>
                                        <div class="c-txt">未完成（个）</div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                        <div class="tit-new">焊接一次合格率统计</div>
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
                                <div class="project-tit-wrap">
                                    <div class="project-tit">— 项目快捷入口 —</div>
                                </div>
                                <div class="project-list" id="divProjectList" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="map-desc">
                            <div class="desc">
                                <div class="d-item">
                                    <div class="tit">工地总数</div>
                                    <div class="num" runat="server" id="numProjetcA"></div>
                                </div>
                                <div class="d-item d-item-1">
                                    <div class="tit">在建</div>
                                    <div class="num" runat="server" id="numProjetc1"></div>
                                </div>
                                <div class="d-item d-item-1">
                                    <div class="tit">停工</div>
                                    <div class="num" runat="server" id="numProjetc2"></div>
                                </div>
                                <div class="d-item">
                                    <div class="tit">竣工</div>
                                    <div class="num" runat="server" id="numProjetc3"></div>
                                </div>
                                <div class="d-item">
                                    <div class="tit">单位：(个)</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="flex1 itemflex2">
                  <div class="flex bw-b-bottom">
                      <div id="swiper-pre" class="bw-item flex1 mbnone" style="flex: 1; width: 48%;">
                          <div class="bw-item-content flex flexV">
                              <div class="tit-new">劳务统计</div>
                              <%--<div class=" flex1">
                                  <div id='Accumulation' style="width: 100%; height: 100%;"></div>
                              </div>--%>
                              <div class="content-wrap tab-content flex1 flex" style="overflow: visible;padding-bottom:0;margin-top:0;">
                                  <%--<div class="Accumulation-next">
                                      <div class="flex tab-h">
                                          <div class="txt">工程名</div>
                                          <div class="txt">在线数</div>
                                      </div>
                                      <div class="flex tab-i">
                                          <div class="txt">工程1</div>
                                          <div class="txt">12</div>
                                      </div>
                                      <div class="flex tab-i">
                                          <div class="txt">工程2</div>
                                          <div class="txt">23</div>
                                      </div>
                                      <div class="flex tab-i">
                                          <div class="txt">工程3</div>
                                          <div class="txt">34</div>
                                      </div>
                                  </div>--%>
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
                              <div class="" style="padding: 0 10px 0; overflow: hidden;">
                                  <div class="swiper-container " id='swiper2' runat="server">
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
                </div>
            </div>
            <!--右侧-->
            <div class="bw-s flex1 flexV flex" style="">
                <div class="bg-item flex1">
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">进度统计</div>
                        <div id="divJD" runat="server" class="content-wrap tab-content flex1" style="overflow: auto;">
                            <div class="flex tab-h">
                                <div class="txt">工地名称</div>
                                <div class="txt">状态</div>
                                <div class="flex1" style="text-align: center">进度</div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地1</div>
                                <div class="txt">在建</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="normal" style="width: 80%"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地2</div>
                                <div class="txt">在建</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="normal" style="width: 50%"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="flex tab-i">
                                <div class="txt">工地3</div>
                                <div class="txt">在建</div>
                                <div class="flex1 flex line-wrap">
                                    <div class="line-item">
                                        <div class="warn" style="width: 100%"></div>
                                    </div>
                                </div>
                            </div>
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
                    <div class="bw-item-content flex flexV">
                        <div class="tit-new">站点链接</div>
                        <div class="content-wrap tab-content flex1" style="overflow: auto;">
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
    //function getProjectList() {
    //    var $divProjectList = document.getElementById("divProjectList");
    //    $divProjectList.innerHTML="<div>宁夏瑞泰集中供热项目</div><div>测试项目</div>";
    //}
    //getProjectList();
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
                    fontSize: 16
                },
                show: false
            },
            tooltip: {},
            legend: {
                left: '3%',
                show: false,
                selectedMode: false,
                textStyle: {//图例文字的样式
                    color: '#ffffff'
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
                    interval: 0,
                    rotate: 50,
                    textStyle: {
                        fontSize:8,
                        color: 'rgba(255, 255, 255, 0.8)'
                    },
                    //formatter: function(params) {
                    //    var newParamsName = ''
                    //    var paramsNameNumber = params.length
                    //    var provideNumber = 6
                    //    var rowNumber = Math.ceil(paramsNameNumber / provideNumber)
                    //    for (let row = 0; row < rowNumber; row++) {
                    //      newParamsName +=
                    //        params.substring(
                    //          row * provideNumber,
                    //          (row + 1) * provideNumber
                    //        ) + '\n'
                    //    }
                    //    return newParamsName
                    //  }
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
    var two =<%=Two %>;
    var three=<%=Three %>;
    var xArr2 = two.categories
    var xArr = three.categories
    var data = [12, 5, 28, 43, 22]
    var data1 = [21, 9, 12, 15, 8]
    var series = [{
        name: '质量一次验收合格率',
        type: 'bar',
        barWidth: 20,
        //barGap:10,
        data: two.series[0].data,
        itemStyle: { normal: { color: 'rgba(43,155,176,1)' } }
    }];
    var series1 = [
        {
            name: '焊接一次合格率',
            type: 'bar',
            barWidth: 20,
            data: three.series[0].data,
            itemStyle: { normal: { color: 'rgba(140,202,214, 1)' } }
        }];
    category('two', xArr2, series)
    category('three', xArr, series1)
</script>
<script type="text/javascript">
    function category_Five(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: 'center',
                text: '',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight: '300'
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
    var xArr = ["类别1", "类别2", "类别3", "类别4", "类别5"]
    var data = [{
        name: '',
        type: 'bar',
        barWidth: 20,
        stack: '总量',
        data: [20, 2, 1, 34, 39],
        itemStyle: { normal: { color: 'rgba(160,181,204, 1)' } }
    },
    {
        name: '',
        type: 'bar',
        stack: '总量',
        data: [12, 32, 10, 14, 9],
        itemStyle: { normal: { color: 'rgba(28,110,173, 1)' } }
    }]
    category_Five('five', xArr, data)
</script>
<script type="text/javascript">
    function line(id, xArr, data) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(id))
        // 指定图表的配置项和数据
        var option = {
            title: {
                left: 'center',
                text: ' ',
                textStyle: {
                    color: '#fff',
                    fontSize: 14,
                    fontWeight: '300'
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
    var data = [{
        name: '项目安全人工时',
        type: 'line',
        //smooth: true,
        data: [3, 5, 2, 3, 4, 2, 9, 8, 4, 7, 6, 1],
        lineStyle: {
            //color: 'rgba(200,201,10, 1)'
        }
    }, {
        name: '项目安全人工时1',
        type: 'line',
        //smooth: true,
        data: [6, 5, 3, 3, 4, 1, 3, 2, 4, 7, 6, 1]
    }, {
        name: '项目安全人工时2',
        type: 'line',
        //smooth: true,
        data: [5, 4, 2, 3, 8, 2, 9, 2, 4, 1, 6, 4]
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
                    interval: 0,
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
    var projectNum =<%=ProjectNum %>
    var myChart = null
    function mapEchart(id, mapType) {
        myChart = echarts.init(document.getElementById(id));
        var mapType = mapType || 'china'
        var mapZoom = mapType == 'china' ? 1.2 : 0.8
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
            series: [
                {
                    type: 'map',
                    // mapType: 'world',
                    mapType: mapType,
                    zoom: mapZoom,
                    roam: false,
                    data: [
                        { name: 'China', value: projectNum.data[0] },
                        { name: 'United States', value: randomData() },
                        { name: '上海', value: projectNum.data[1] },
                        { name: '河北', value: projectNum.data[2] },
                        { name: '山西', value: projectNum.data[3] },
                        { name: '内蒙古', value: projectNum.data[4] },
                        { name: '辽宁', value: projectNum.data[5] },
                        { name: '吉林', value: projectNum.data[6] },
                        { name: '黑龙江', value: projectNum.data[7] },
                        { name: '江苏', value: projectNum.data[8] },
                        { name: '浙江', value: projectNum.data[9] },
                        { name: '安徽', value: projectNum.data[10] },
                        { name: '福建', value: projectNum.data[11] },
                        { name: '江西', value: projectNum.data[12] },
                        { name: '山东', value: projectNum.data[13] },
                        { name: '河南', value: projectNum.data[14] },
                        { name: '湖北', value: projectNum.data[15] },
                        { name: '湖南', value: projectNum.data[16] },
                        { name: '广东', value: projectNum.data[17] },
                        { name: '广西', value: projectNum.data[18] },
                        { name: '海南', value: projectNum.data[19] },
                        { name: '四川', value: projectNum.data[20] },
                        { name: '贵州', value: projectNum.data[21] },
                        { name: '云南', value: projectNum.data[22] },
                        { name: '西藏', value: projectNum.data[23] },
                        { name: '陕西', value: projectNum.data[24] },
                        { name: '甘肃', value: projectNum.data[25] },
                        { name: '青海', value: projectNum.data[26] },
                        { name: '宁夏', value: projectNum.data[27] },
                        { name: '新疆', value: projectNum.data[28] },
                        { name: '北京', value: projectNum.data[29] },
                        { name: '天津', value: projectNum.data[30] },
                        { name: '重庆', value: projectNum.data[31] },
                        { name: '香港', value: projectNum.data[32] },
                        { name: '澳门', value: projectNum.data[33] }
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
    var swiper_One = '<%=swiper_One %>'
    var swiper_Two = '<%=swiper_Two %>'
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
            if (index == 0) {
                $("#swiper2").html(swiper_One)
                mySwiper = new Swiper('#swiper2', {
                    autoplay: 4000,//可选选项，自动滑动
                    direction: 'vertical',
                    loop: true,
                    slidesPerView: slidesNum
                })
            } else if (index == 2) {
                $("#swiper2").html(swiper_Two)
                mySwiper = new Swiper('#swiper2', {
                    autoplay: 4000,//可选选项，自动滑动
                    direction: 'vertical',
                    loop: true,
                    slidesPerView: slidesNum
                })
            }
        }
    })

    $("#swiper2").on('click', 'li', function () {
        var $this = $(this)
        var data = $this.attr("data-id")
        window.open(data)
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
    var widthNow = ((document.body.offsetWidth - 30) / 2) * 0.48
    //alert(widthNow)
    var leftAccValue = '100'
    var leftoffsetValue = 150
    var leftoffsetValue1 = 60
    if (widthNow < 450) {
       leftAccValue = '20'
       leftoffsetValue = 80
       leftoffsetValue1 = 20
    }
//alert(leftAccValue + "," + leftoffsetValue + "," +leftoffsetValue1)
    function Accumulation(id) {
        var myChartLine = echarts.init(document.getElementById(id));
        option = {
            title: {
                show: false
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'line'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                show: false
            },
            grid: {
              top: '2%',
              left: leftAccValue,
              right: '2%',
              bottom: '0',
              containLabel: true
            },
            xAxis: {
                show: false,
                type: 'value'
            },
            yAxis: [{
                offset: leftoffsetValue,
                position: 'left',
                type: 'category',
		
                data: ['项目一项目目', '项目二', '项目三', '项目四', '项目五', '项目六', '项目名称'],
                axisLabel: {
		    interval:0,
                    show: true,
                    fontSize: 12,
                    color: "#fff",
                    align: 'middle',
                    verticalAlign: 'middle',
                    formatter: function (value) {
                        var ret = "";//拼接加\n返回的类目项  
                        var maxLength = 5;//每项显示文字个数  
                        var valLength = value.length;//X轴类目项的文字个数  
                        var rowN = Math.ceil(valLength / maxLength); //类目项需要换行的行数  
                        if (rowN > 1)//如果类目项的文字大于maxLength,  
                        {
                            for (var i = 0; i < rowN; i++) {
                                var temp = "";//每次截取的字符串  
                                var start = i * maxLength;//开始截取的位置  
                                var end = start + maxLength;//结束截取的位置  
                                //这里也可以加一个是否是最后一行的判断，但是不加也没有影响，那就不加吧  
                                temp = value.substring(start, end) + (i == rowN - 1 ? "" : "\n");
                                ret += temp; //凭借最终的字符串  
                            }
                            return ret;
                        }
                        else {
                            return value;
                        }
                    }
                },
                axisTick: { show: false },
                splitLine: { show: false },
                axisLine: { show: false }
            }, {
                offset: leftoffsetValue1,
                position: 'left',
                type: 'category',
                data: ['20', '30', '30', '32', '30', '30', '在线数'],
                axisLabel: {
                    show: true,
                    fontSize: 12,
                    color: "#fff",
                    align: 'middle',
                    verticalAlign: 'middle',
                    formatter: function (value) {
                        var ret = "";//拼接加\n返回的类目项  
                        var maxLength = 5;//每项显示文字个数  
                        var valLength = value.length;//X轴类目项的文字个数  
                        var rowN = Math.ceil(valLength / maxLength); //类目项需要换行的行数  
                        if (rowN > 1)//如果类目项的文字大于maxLength,  
                        {
                            for (var i = 0; i < rowN; i++) {
                                var temp = "";//每次截取的字符串  
                                var start = i * maxLength;//开始截取的位置  
                                var end = start + maxLength;//结束截取的位置  
                                //这里也可以加一个是否是最后一行的判断，但是不加也没有影响，那就不加吧  
                                temp = value.substring(start, end) + (i == rowN - 1 ? "" : "\n");
                                ret += temp; //凭借最终的字符串  
                            }
                            return ret;
                        }
                        else {
                            return value;
                        }
                    }
                },
                axisTick: { show: false },
                splitLine: { show: false },
                axisLine: { show: false },

            }],
            series: [
                {
                    name: '当前现场人数',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
<<<<<<< HEAD
                    data:  data1
=======
                    data: [320, 302, 301, 334, 390, 330]
>>>>>>> 43f031eed793f0bbda7af444e70d062240ffc332
                },
                {
                    name: '作业人员总数',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
<<<<<<< HEAD
                    data: data2
=======
                    data: [120, 132, 101, 134, 90, 230]
>>>>>>> 43f031eed793f0bbda7af444e70d062240ffc332
                },
                {
                    name: '管理人员总数',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        show: true,
                        position: 'insideRight'
                    },
                    //barWidth: 30,
                    //barGap: 0,
<<<<<<< HEAD
                    data: data3
=======
                    data: [220, 182, 191, 234, 290, 330]
>>>>>>> 43f031eed793f0bbda7af444e70d062240ffc332
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
<script type="text/javascript">
    var slidesNum = 5
    var mySwiper = null
    $(document).ready(function () {

        var height = $("#swiper-pre").height() - 29
        $("#swiper2").css("height", (height) + 'px')
        slidesNum = Math.floor((height) / 24)
        $("#swiper2").html(swiper_One)
        mySwiper = new Swiper('#swiper2', {
            autoplay: 4000,//可选选项，自动滑动
            direction: 'vertical',
            loop: true,
            slidesPerView: slidesNum
        })

        $(".project").hover(function () {
            $(".project-list").show();
        }, function () {
            $(".project-list").hide();
        });

        $(".project-list>div").click(function () {
            var $this = $(this)
            top.window.location.href = "../indexProject.aspx?projectName=" + $this.html();
        });
    })   
</script>
</html>
