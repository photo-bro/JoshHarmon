import React, { Component } from 'react';
import Modal from 'react-modal';
import Tool from './Tool';
import { ProjectModalDetail } from './ProjectModalDetail';

Modal.setAppElement('#root')

export class Project extends Component {
    static displayName = Project.name;

    constructor(props) {
        super(props);
        this.state = {
            modalOpen: false,
            name: props.model.name,
            repositoryName: props.model.repositoryName,
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
        let shortDescr = this.state.content.slice(0, 64) + ' ...';

        return (
            <div>
                <div className="project flex flex-column full-center shadow-box-soft" onClick={this.toggleModal}>
                    <h1 className="text-center thin ml10 mr10">
                        {this.state.name}
                    </h1>
                    <p>
                        {shortDescr}
                    </p>
                    <div className="flex flex-row flex-wrap">
                        {tools}
                    </div>
                </div>
                <Modal
                    className="modal-main"
                    overlayClassName="modal-overlay flex full-center"
                    isOpen={this.state.modalOpen}
                    shouldCloseOnOverlayClick={true}
                    shouldCloseOnEsc={true}
                    onRequestClose={() => this.setState({ modalOpen: false })}
                    onAfterOpen={this.afterOpen}
                    contentLabel={this.state.title}>
                    <ProjectModalDetail
                        repositoryName={this.state.repositoryName}
                        repositoryUrl={this.state.externalUrl}
                        name={this.state.name}
                        content={this.state.content}
                        tools={tools}
                    />
                </Modal>
            </div>
        );
    }
}