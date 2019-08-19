import React, { Component } from 'react';
import Modal from 'react-modal';

const projectModalStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)'
    }
}

Modal.setAppElement('#root')

export class Project extends Component {
    static displayName = Project.name;

    constructor(props) {
        super(props);
        this.state = {
            modalOpen: false,
            name: props.model.name,
            iconUrl: props.model.iconUrl,
            mediaUrl: props.model.mediaUrl,
            content: props.model.content
        };

        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.afterOpen = this.afterOpen.bind(this);
    }

    openModal() {
        this.setState({ modalOpen: true });
    }

    closeModal() {
        this.setState({ modalOpen: false });
    }

    afterOpen() {

    }

    render() {
        return (
            <div class="project">
                <img src={this.state.iconUrl} />
                <div class="projectName" onClick={this.openModal}>
                    {this.state.name}
                </div>

                <Modal
                    isOpen={this.state.modalOpen}
                    onAfterOpen={this.afterOpen}
                    style={projectModalStyles}
                    contentLabel={this.state.title}>
                    <div class="projectModal" onClick={this.closeModal}>
                        <h1>{this.state.name}</h1>
                        <img src={this.state.mediaUrl} />
                        <p>{this.state.content}</p>
                    </div>
                </Modal>
            </div>
        );
    }

}