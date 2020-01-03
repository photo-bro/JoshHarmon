import React, { Component } from 'react';
import Accordion from 'react-bootstrap/Accordion';
import Card from 'react-bootstrap/Card';
import CardDeck from 'react-bootstrap/CardDeck';
import ListGroup from 'react-bootstrap/ListGroup';
import ListGroupItem from 'react-bootstrap/ListGroupItem';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import { PrettyDate } from '../Shared/PrettyDate';
import { DateDelta } from '../Shared/DateDelta';
import { SkillsGraph } from './SkillsGraph';

import './about-me.css';


export class AboutMe extends Component {
    static displayName = AboutMe.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class="page">
                <div class="aboutMe">
                    <img class="aboutMeBanner" src="/assets/JoshOutOfFocus.jpg" />
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper. Suspendisse mattis sapien nec eros ornare euismod at quis massa. Aenean sit amet vulputate dolor. Praesent placerat hendrerit fringilla. Pellentesque aliquet lobortis porta. Fusce euismod rutrum orci tempor blandit. Vivamus gravida lobortis justo, non suscipit nulla gravida sit amet. Nunc sodales risus non consequat congue. Nulla nulla ipsum, mattis eu mi vitae, semper vestibulum velit. Aenean condimentum purus sed neque cursus semper. Maecenas blandit in tortor efficitur mattis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris ullamcorper id urna sed tempus.
                    </p>
                    <Tabs
                        className="tabs"
                        selectedTabClassName="tabSelected"
                        selectedTabPanelClassName="panelSelected">
                        <TabList className="tabList">
                            <Tab className="tab">Professional</Tab>
                            <Tab className="tab">Personal</Tab>
                        </TabList>
                        <TabPanel className="tabPanel">
                            {/* Professional */}
                            <Accordion defaultActiveKey="0">
                                <Card>
                                    <Accordion.Toggle as={Card.Header} eventKey="0">Experience</Accordion.Toggle>
                                    <Accordion.Collapse eventKey="0">
                                        <Card.Body>
                                            <CardDeck>
                                                <Card>
                                                    <Card.Header>Emburse</Card.Header>
                                                    <Card.Img variant="top" src="/icon/emburse.png" as="img" />
                                                    <Card.Title>Software Engineer</Card.Title>
                                                    <Card.Body>
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</p>
                                                        <ListGroup>
                                                            <ListGroupItem>Python + Django</ListGroupItem>
                                                            <ListGroupItem>Postgres SQL</ListGroupItem>
                                                            <ListGroupItem>AWS</ListGroupItem>
                                                        </ListGroup>
                                                    </Card.Body>
                                                    <Card.Footer>
                                                        <b>{PrettyDate.buildFormattedDateTimeString("2019-12-06", false)}</b>
                                                        &nbsp; to &nbsp;
                                                        <b>{PrettyDate.buildFormattedDateTimeString(new Date().toISOString(), false)}</b>
                                                        <DateDelta leftDate="2019-12-06" rightDate={new Date().toISOString()} />
                                                    </Card.Footer>
                                                </Card>
                                                <Card>
                                                    <Card.Header>Xero</Card.Header>
                                                    <Card.Img variant="top" src="/icon/xero.png" />
                                                    <Card.Title>Software Engineer</Card.Title>
                                                    <Card.Body>
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</p>
                                                        <ListGroup>
                                                            <ListGroupItem>Javascript + React</ListGroupItem>
                                                            <ListGroupItem>C# + ASP.NET + .Net Core</ListGroupItem>
                                                            <ListGroupItem>Postgres SQL + MySql + MS SQL</ListGroupItem>
                                                            <ListGroupItem>AWS</ListGroupItem>
                                                        </ListGroup>
                                                    </Card.Body>
                                                    <Card.Footer>
                                                        <b>{PrettyDate.buildFormattedDateTimeString("2017-10-02", false)}</b>
                                                        &nbsp; to &nbsp;
                                                        <b>{PrettyDate.buildFormattedDateTimeString("2019-10-18", false)}</b>
                                                        <DateDelta leftDate="2017-10-02" rightDate="2019-10-18" />
                                                    </Card.Footer>
                                                </Card>
                                                <Card>
                                                    <Card.Header>Ensenta</Card.Header>
                                                    <Card.Img variant="top" src="/icon/ensenta.png" />
                                                    <Card.Title>Software Engineer</Card.Title>
                                                    <Card.Body>
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</p>
                                                        <ListGroup>
                                                            <ListGroupItem>C# + ASP.NET + .Net Framework</ListGroupItem>
                                                            <ListGroupItem>MS SQL</ListGroupItem>
                                                        </ListGroup>
                                                    </Card.Body>
                                                    <Card.Footer>
                                                        <b>{PrettyDate.buildFormattedDateTimeString("2015-07-02", false)}</b>
                                                        &nbsp; to &nbsp;
                                                        <b>{PrettyDate.buildFormattedDateTimeString("2017-10-02", false)}</b>
                                                        <DateDelta leftDate="2015-07-01" rightDate="2017-10-01" />
                                                    </Card.Footer>
                                                </Card>
                                            </CardDeck>
                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                                <Card>
                                    <Accordion.Toggle as={Card.Header} eventKey="1">Skills</Accordion.Toggle>
                                    <Accordion.Collapse eventKey="1">
                                        <Card.Body>
                                            <h2>Languages</h2>
                                            <SkillsGraph />
                                            <h2>Technologies</h2>
                                            <SkillsGraph />
                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                                <Card>
                                    <Accordion.Toggle as={Card.Header} eventKey="2">Education</Accordion.Toggle>
                                    <Accordion.Collapse eventKey="2">
                                        <Card.Body>

                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                            </Accordion>
                        </TabPanel>
                        <TabPanel className="tabPanel">
                            {/* Personal */}

                        </TabPanel>
                    </Tabs>
                </div>
            </div >
        );
    }
}
