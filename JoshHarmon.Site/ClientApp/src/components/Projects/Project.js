import React, { Component } from 'react';
import Modal from 'react-modal';
import Tool from './Tool';

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
            externalUrl: props.model.externalUrl,
            content: props.model.content,
            tools: props.model.tools
        };

        this.toggleModal = this.toggleModal.bind(this);
        this.afterOpen = this.afterOpen.bind(this);
    }

    toggleModal() {
        this.setState({ modalOpen: !this.state.modalOpen });
    }

    afterOpen() {
        this.setState({ modalOpen: true });
    }

    render() {
        let tools = this.state.tools.map(t => <Tool model={t} />);

        return (
            <div class="project">
                <img src={this.state.iconUrl} onClick={this.toggleModal} />
                <div class="projectName">
                    {this.state.name}
                </div>

                <Modal
                    className="modalMain"
                    overlayClassName="modalOverlay"
                    isOpen={this.state.modalOpen}
                    shouldCloseOnOverlayClick={true}
                    shouldCloseOnEsc={true}
                    onRequestClose={() => this.setState({ modalOpen: false })}
                    onAfterOpen={this.afterOpen}
                    contentLabel={this.state.title}>
                    <div class="modalContent">
                        <a href={this.state.externalUrl} target="_blank" class="modalContentItem">
                            <img src={this.state.mediaUrl} class="modalImg" />
                        </a>
                        <div className="modalDescription modalContentItem">
                            <h1>{this.state.name}</h1>
                            <div className="modalTools">
                                {tools}
                            </div>
                            <p>{this.state.content}</p>
                        </div>
                    </div>
                </Modal>
            </div>
        );
    }
}