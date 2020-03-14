import React, { Component } from 'react';
import { defaults, HorizontalBar } from 'react-chartjs-2';


defaults.easing = 'easeInOutBounce';


export class SkillsGraph extends Component {
    static displayName = SkillsGraph.name;

    constructor(props) {
        super(props);

        this.data = {
            labels: ["C#", "Sql", "Python", "JavaScript"],
            datasets: [
                {
                    label: "Years",
                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                    borderColor: "#eeeeee",
                    borderWidth: 1,
                    data: [6, 6, 3, 3, 0]
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
