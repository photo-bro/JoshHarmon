import React, { Component } from 'react';
import Modal from 'react-modal';

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
                    className="modalMain"
                    overlayClassName="modalOverlay"
                    isOpen={this.state.modalOpen}
                    onAfterOpen={this.afterOpen}
                    shouldCloseOnOverlayClick={true}
                    shouldCloseOnEsc={true}
                    contentLabel={this.state.title}>
                    <div class="modalContent" onClick={this.closeModal}>
                        <img src={this.state.mediaUrl} />
                        <div className="modalDescription">
                            <h1>{this.state.name}</h1>
                            <p>{this.state.content}</p>
                        </div>
                    </div>
                </Modal>
            </div>
        );
    }

}