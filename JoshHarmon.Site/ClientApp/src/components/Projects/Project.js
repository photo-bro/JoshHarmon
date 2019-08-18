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
            title: props.title,
            mediaUrl: props.mediaUrl
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
                <img src={this.state.mediaUrl} />
                <div class="projectTitle" onClick={this.openModal}>
                    <p>{this.state.title}</p>
                </div>

                <Modal
                    isOpen={this.state.modalOpen}
                    onAfterOpen={this.afterOpen}
                    style={projectModalStyles}
                    contentLabel={this.state.title}
                >
                    <div class="projectModal" onClick={this.closeModal}>
                        <h1>Modal Open</h1>
                    </div>
                    </Modal>
            </div>
        );
    }

}