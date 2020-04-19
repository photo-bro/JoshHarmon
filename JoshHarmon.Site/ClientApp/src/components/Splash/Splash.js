import React, { Component } from 'react';
import { VisualPanel } from './VisualPanel';


export class Splash extends Component {
    static displayName = Splash.name;

    constructor() {
        super();
        this.state = {
            panels: [],
            loading: true,
            loadingMessage: "Loading..."
        };

        fetch('api/splash', { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    panels: data.panels,
                    loading: false
                });
            });
    }

    static buildVisualPanels(panelModels) {
        return (
            <div>
                {panelModels.map(model => <VisualPanel model={model} />)}
            </div>
        );
    }

    render() {
        let panelContent = this.state.loading
            ? <h3>{this.state.loadingMessage}</h3>
            : Splash.buildVisualPanels(this.state.panels);

        return (
            <div className="page">
                {panelContent}
            </div>
        );
    }
}