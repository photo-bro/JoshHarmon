import React, { Component } from 'react';
import { defaults, HorizontalBar } from 'react-chartjs-2';


defaults.easing = 'easeInOutBounce';


export class SkillsGraph extends Component {
    static displayName = SkillsGraph.name;

    constructor(props) {
        super(props);

        // Append empty item to format graph correctly
        //props.data.push({
        //    label: null,
        //    color: '#000000',
        //    data: 0
        //});

        const labels = props.data.map(d => d.label);
        const values = props.data.map(d => d.value);
        values.push(0);  // Add 0 to set x-scale properly
        const colors = props.data.map(d => d.color);

        console.log(props.data); // !!!
        console.log(labels); // !!!
        console.log(values); // !!!
        console.log(colors); // !!!

        this.data = {
            labels: labels, //["C#", "Sql", "Python", "JavaScript"],
            datasets: [
                {
                    label: "Years",
                    backgroundColor: colors,// ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                    borderColor: "#eeeeee",
                    borderWidth: 1,
                    data: values //[6, 6, 3, 3, 0]
                }
            ]

        };

        this.options = {
            legend: { display: false },
            title: {
                display: false
            },
            scales: {
                yAxes: [{
                    gridLines: { display: false }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Years",
                    },
                    gridLines: { display: false }
                }],
            },
            maintainAspectRatio: false
        }
    }

    render() {
        return (
            <div className="graph">
                <HorizontalBar data={this.data} options={this.options} redraw={true} />
            </div>
        );
    }


}
