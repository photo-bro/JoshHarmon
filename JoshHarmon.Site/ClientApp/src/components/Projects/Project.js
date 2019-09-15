import React, { Component } from 'react';
import Modal from 'react-modal';
import Tool from './Tool';
import { RepoStats } from './RepoStats';

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
                <div class="project" onClick={this.toggleModal}>
                    <div class="projectName">
                        {this.state.name}
                    </div>
                    <p>
                        {shortDescr}
                    </p>
                    <div className="tools">
                        {tools}
                    </div>
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
                        <div className="modalContentItem">
                            <RepoStats repoName={this.state.repositoryName} />
                        </div>
                        <div className="modalDescription modalContentItem">
                            <h1>{this.state.name}</h1>
                            <div className="tools">
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