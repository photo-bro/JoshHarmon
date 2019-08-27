﻿import React, { Component } from 'react';
import { Collapse, Container } from 'reactstrap';
import { VisualPanel } from './VisualPanel';
import { FootBar } from '../Shared/FootBar';


export class Splash extends Component {
    static displayName = Splash.name;

    constructor() {
        super();
        this.state = {
            panelModels: [],
            connectModels: [],
            loading: true,
            loadingMessage: "Loading..."
        };

        fetch('api/splash', { method: 'get' })
            .then(response => response.json())
            .then(data => {               
                this.setState({
                    panelModels: data.panelModels,
                    connectModel: data.connectModel,
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
            : Splash.buildVisualPanels(this.state.panelModels);

        return (
            <div class="page">
                {panelContent}
            </div>
        );
    }
}