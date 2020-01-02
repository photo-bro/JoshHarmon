﻿import React, { Component } from 'react';
import Accordion from 'react-bootstrap/Accordion';
import Card from 'react-bootstrap/Card';
import CardDeck from 'react-bootstrap/CardDeck';
import ListGroup from 'react-bootstrap/ListGroup';
import ListGroupItem from 'react-bootstrap/ListGroupItem';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';

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
                    <img src="/assets/JoshOutOfFocus.jpg" />
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
                                            <div>
                                                <div>
                                                    <CardDeck>
                                                        <Card>
                                                            <Card.Header>Emburse</Card.Header>
                                                            {/*<Card.Img variant="top" src="/" />*/}
                                                            <Card.Title>Software Engineer</Card.Title>
                                                            <Card.Body>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</Card.Body>
                                                            <ListGroup>
                                                                <ListGroupItem>Python + Django</ListGroupItem>
                                                                <ListGroupItem>Postgres SQL</ListGroupItem>
                                                                <ListGroupItem>AWS</ListGroupItem>
                                                            </ListGroup>
                                                            <Card.Footer>December 2019 - Present</Card.Footer>
                                                        </Card>
                                                        <Card>
                                                            {/*<Card.Img variant="top" src="/" />*/}
                                                            <Card.Title>Xero</Card.Title>
                                                            <Card.Subtitle>Software Engineer</Card.Subtitle>
                                                            <Card.Body>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</Card.Body>
                                                            <ListGroup>
                                                                <ListGroupItem>React JS</ListGroupItem>
                                                                <ListGroupItem>C# + ASP.NET + .Net Core</ListGroupItem>
                                                                <ListGroupItem>PostGres SQL + MySql + MS SQL</ListGroupItem>
                                                                <ListGroupItem>AWS</ListGroupItem>
                                                            </ListGroup>
                                                            <Card.Footer>October 2017 - October 2019</Card.Footer>
                                                        </Card>
                                                        <Card>
                                                            {/*<Card.Img variant="top" src="/" />*/}
                                                            <Card.Title>Ensenta</Card.Title>
                                                            <Card.Subtitle>Software Engineer</Card.Subtitle>
                                                            <Card.Body>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper.</Card.Body>
                                                            <ListGroup>
                                                                <ListGroupItem>C# + ASP.NET + .Net Framework</ListGroupItem>
                                                                <ListGroupItem>MS SQL</ListGroupItem>
                                                            </ListGroup>
                                                            <Card.Footer>July 2015 - October 2017</Card.Footer>
                                                        </Card>
                                                    </CardDeck>
                                                </div> {/* left */}
                                                <div></div> {/* right */}
                                            </div>
                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                                <Card>
                                    <Accordion.Toggle as={Card.Header} eventKey="1">Skills</Accordion.Toggle>
                                    <Accordion.Collapse eventKey="1">
                                        <Card.Body>

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
            </div>
        );
    }
}