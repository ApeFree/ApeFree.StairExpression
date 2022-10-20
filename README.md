# StairExpression
> 最后一次修改于 2022年9月27日

[TOC]

## 简介
StairExpression是一种用于描述树形数据集合中指定节点位置的表达式解析库。
此库可将表达式字符串语句解析出查询参数数据结构，使用此查询参数作为逐层检索树形数据集合的依据，实现对目标节点的快速定位。

---

## 语法

### 示例

查询语句：

```StairExpression
第一层节点.第二层节点[条件属性1=比较值1,条件属性2="比较值2"].$属性节点
```

语句解析结果：

```json
{
  "Expression": "第一层节点.第二层节点[条件属性1=比较值1,条件属性2=比较值2].$属性节点",
  "Nodes": [
    {
      "Index": 0,                                                           // 节点序号
      "IsAttribute": false,                                                 // 是否是属性节点
      "IsElement": true,                                                    // 属否是元素节点
      "NodeExpression": "第一层节点",                                        // 节点表达式
      "NodeName": "第一层节点",                                              // 节点名称
      "QueryConditions": null                                               // 查询条件
    },
    {
      "Index": 1,
      "IsAttribute": false,
      "IsElement": true,
      "NodeExpression": "第二层节点[条件属性1=比较值1,条件属性2=比较值2]",
      "NodeName": "第二层节点",
      "QueryConditions": [
        {
          "ConditionExpression": "条件属性1=比较值1",                         // 条件表达式
          "Key": "属性名1",                                                  // 属性名称
          "Value": "比较值1"                                                 // 比较值
        },
        {
          "ConditionExpression": "条件属性2=比较值2",
          "Key": "属性名2",
          "Value": "比较值2"
        }
      ]
    },
    {
      "Index": 2,
      "IsAttribute": true,
      "IsElement": false,
      "NodeExpression": "$属性节点",
      "NodeName": "属性节点",
      "QueryConditions": null
    }
  ],
  "NodeCount": 3
}
```

---

## StairExpression.Xml

### 介绍
将StairExpression表达式应用于检索Xml的节点，支持在描述节点名称和筛选条件值时使用通配符。
该库只依赖于表达式解析库和`System.Xml.Linq`，

### 示例

待操作的XML文档如下：
```xml
<?xml version="1.0" encoding="utf-8"?>
<school>
    <class name="101" type="理科">
        <teacher subject="数学">Lisa</teacher>
        <student age="16" sex="0">Barbara</student>
        <student age="17" sex="0">Sucrose</student>
        <student age="16" sex="1">Bennett</student>
    </class>
    <class name="102" type="文科">
        <teacher subject="语文">Jean</teacher>
        <student age="16" sex="0">Fischl</student>
        <student age="17" sex="1">Razor</student>
        <student age="16" sex="0">Klee</student>
    </class>
    <department name="后勤科">
        <staff>Noelle</staff>
    </department>
    <department name="保卫科">
        <staff>Kaeya</staff>
    </department>
</school>
```

#### 元素定位
获取所有教师的元素节点。

表达式: `school.class.teacher`

执行结果:
```
结果1:
<teacher subject="数学">Lisa</teacher>

文档节点路径：school.class.teacher
---------------------------------------------------
结果2:
<teacher subject="语文">Jean</teacher>

文档节点路径：school.class.teacher
---------------------------------------------------
```

#### 属性定位
获取教师专业课程的属性节点。

表达式: `school.class.teacher.$subject`

执行结果:
```
结果1:
subject="数学"

文档节点路径：school.class.teacher.subject
---------------------------------------------------
结果2:
subject="语文"

文档节点路径：school.class.teacher.subject
---------------------------------------------------
```

#### 条件检索(属性值)
查找名为“Jean”的教师。

表达式: `school.class.teacher[$text=Jean]`

执行结果:
```
结果1:
<teacher subject="语文">Jean</teacher>

文档节点路径：school.class.teacher
```

#### 条件检索(内部文本)
查找名为“Jean”的教师。

表达式: `school.class.teacher[$text=Jean]`

执行结果:
```
结果1:
<teacher subject="语文">Jean</teacher>

文档节点路径：school.class.teacher
```

#### 多节的条件检索
查询102班内所有年龄为17的学生。
表达式: `school.class[name=102].student[age=16]`

执行结果:
```
结果1:
<student age="16" sex="0">Fischl</student>

文档节点路径：school.class.student
---------------------------------------------------
结果2:
<student age="16" sex="0">Klee</student>

文档节点路径：school.class.student
---------------------------------------------------
```

#### 多条件检索
查询所有年龄为17岁的男学生。
表达式: `school.class.student[age=17,sex=1]`

执行结果:
```
结果1:
<student age="17" sex="1">Razor</student>

文档节点路径：school.class.student
```

#### 通配符查询
表达式: `school.*.*[$text=Noel*]`

执行结果:
```
结果1:
<staff>Noelle</staff>

文档节点路径：school.department.staff
```

<!-- #### 基本定位
表达式: ``

执行结果:
```

``` -->
