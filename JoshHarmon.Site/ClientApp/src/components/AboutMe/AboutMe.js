import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import CardDeck from 'react-bootstrap/CardDeck';
import ListGroup from 'react-bootstrap/ListGroup';
import ListGroupItem from 'react-bootstrap/ListGroupItem';
import { PrettyDate } from '../Shared/PrettyDate';
import { DateDelta } from '../Shared/DateDelta';
import { SkillsGraph } from './SkillsGraph';

import './about-me.css';

export class AboutMe extends Component {
    static displayName = AboutMe.name;

    static getLanguageSkillsDataset() {
        return [
            {
                label: 'C#',
                color: '#3e95cd',
                value: 6,
            },
            {
                label: 'SQL',
                color: '#8e5ea2',
                value: 8,
            },
            {
                label: 'Python',
                color: '#3cba9f',
                value: 7,
            },
            {
                label: 'JavaScript',
                color: '#e8c3b9',
                value: 4,
            },
        ];
    }

    static getTechSkillsDataset() {
        return [
            {
                label: '.Net / ASP.NET',
                color: '#3e95cd',
                value: 6,
            },
            {
                label: 'Django',
                color: '#3cba9f',
                value: 4,
            },
            {
                label: 'React',
                color: '#e8c3b9',
                value: 3,
            },
            {
                label: 'jQuery',
                color: '#8e5ea2',
                value: 2,
            },
            {
                label: 'VueJs',
                color: '#3e95cd',
                value: 2,
            },
        ];
    }

    render() {
        return (
            <div className='page'>
                <div className='aboutMe'>
                    <img className='aboutMeBanner' src='/assets/JoshOutOfFocus.jpg' alt='Josh Harmon' />
                    <h1>
                        <center>Software Engineer + Visual Artist</center>
                    </h1>
                    <p>
                        I am a software engineer with experience in the financial data space - both in payments and data
                        aggregation. If I had to use a single title I would describe myself as a financial integrations
                        engineer.
                        <br />
                        <br />
                        Over the years I've architected, built, and supported a large variety of apps and services
                        ranging from realtime payment processors to user facing interactive webapps. While I am most
                        comfortable working behind the scenes constructing services and components I also enjoy
                        occasionally designing and updating user facing web apps and experiences.
                        <br />
                        <br />
                        Working in the payment and financial data space I’ve acquired experience working with large
                        scale systems and demanding external parties in addition to cultivating a sense for how
                        corporate scale projects evolve. In my career I’ve had the opportunity to work directly with
                        financial service providers including Stripe, American Express, and Mastercard as well as data
                        aggregation platforms such as Plaid and Finicity.
                        <br />
                        <br />
                        Along the way I've worked with many different software stacks but am primarily familiar with the .NET
                        and the Python Django ecosystems. I prefer working with PostgresSQL when needing a RDBMS, Redis for
                        available key value storage, and DynamoDB or MongoDB for object storage. For deploying processes I am
                        familiar with the common containerized and orchestrated deployment processes with Docker and Kubernetes.
                        Regarding observability and APM I prefer the use of Open Telemetry with aggregators such as Datadog or
                        Coralogix but am familiar with any Grafana based dashboards. When it comes to hosting much of my career
                        has been with using AWS however I have experience with alternative and more bespoke solutions, such as
                        this website which is deployed on a Raspberry Pi. All that said I take pride in being knowledgeable of
                        every aspect of an app or services lifecycle.
                        <br />
                        <br />
                        Outside of my professional life I am a primarily film based photographer and avid lifelong swimmer.
                        More information and examples of my visual work can be found on my media website,{' '}
                        <a href='https://joshharmonimages.com' target='_blank' rel='noopener noreferrer'>
                            joshharmonimages.com
                        </a>
                        .
                    </p>
                    <div className='aboutMeContent'>
                        <Card>
                            <Card.Body>
                                <h2>
                                    <span>Work Experience</span>
                                </h2>
                                <CardDeck>
                                    <Card>
                                        <Card.Header>Emburse</Card.Header>
                                        <Card.Img variant='top' src='/icon/emburse.png' as='img' />
                                        <Card.Title>Senior Software Engineer</Card.Title>
                                        <Card.Body>
                                            <p>Lead technical integrations with financial partners and services</p>
                                            <br />
                                            <br />
                                            <ListGroup>
                                                <ListGroupItem>ReactJs</ListGroupItem>
                                                <ListGroupItem>Python 3 + Django</ListGroupItem>
                                                <ListGroupItem>Postgres SQL + Redis</ListGroupItem>
                                                <ListGroupItem>AWS</ListGroupItem>
                                            </ListGroup>
                                        </Card.Body>
                                        <Card.Footer>
                                            <b>{PrettyDate.buildFormattedDateTimeString('2019-12-06', false)}</b>
                                            &nbsp; to &nbsp;
                                            <b>
                                                {PrettyDate.buildFormattedDateTimeString(
                                                    new Date().toISOString(),
                                                    false
                                                )}
                                            </b>
                                            <DateDelta leftDate='2019-12-06' rightDate={new Date().toISOString()} />
                                        </Card.Footer>
                                    </Card>
                                    <Card>
                                        <Card.Header>Xero</Card.Header>
                                        <Card.Img variant='top' src='/icon/xero.png' />
                                        <Card.Title>Software Engineer</Card.Title>
                                        <Card.Body>
                                            <p>
                                                Build and support distributed banking and accounting data integration
                                                and aggregation pipeline
                                            </p>
                                            <br />
                                            <br />
                                            <ListGroup>
                                                <ListGroupItem>ReactJS</ListGroupItem>
                                                <ListGroupItem>C# + ASP.NET + .Net Core</ListGroupItem>
                                                <ListGroupItem>Postgres SQL + MySql + MS SQL</ListGroupItem>
                                                <ListGroupItem>AWS</ListGroupItem>
                                            </ListGroup>
                                        </Card.Body>
                                        <Card.Footer>
                                            <b>{PrettyDate.buildFormattedDateTimeString('2017-10-02', false)}</b>
                                            &nbsp; to &nbsp;
                                            <b>{PrettyDate.buildFormattedDateTimeString('2019-10-18', false)}</b>
                                            <DateDelta leftDate='2017-10-02' rightDate='2019-10-18' />
                                        </Card.Footer>
                                    </Card>
                                    <Card>
                                        <Card.Header>Ensenta</Card.Header>
                                        <Card.Img variant='top' src='/icon/ensenta.png' />
                                        <Card.Title>Software Engineer</Card.Title>
                                        <Card.Body>
                                            <p>
                                                Maintain and extend payment processor infrastructure and back office
                                                tooling
                                            </p>
                                            <br />
                                            <br />
                                            <ListGroup>
                                                <ListGroupItem>jQuery</ListGroupItem>
                                                <ListGroupItem>C# + ASP.NET + .Net Framework</ListGroupItem>
                                                <ListGroupItem>MS SQL</ListGroupItem>
                                            </ListGroup>
                                        </Card.Body>
                                        <Card.Footer>
                                            <b>{PrettyDate.buildFormattedDateTimeString('2015-07-02', false)}</b>
                                            &nbsp; to &nbsp;
                                            <b>{PrettyDate.buildFormattedDateTimeString('2017-10-02', false)}</b>
                                            <DateDelta leftDate='2015-07-01' rightDate='2017-10-01' />
                                        </Card.Footer>
                                    </Card>
                                </CardDeck>
                            </Card.Body>
                        </Card>
                        <Card>
                            <Card.Body>
                                <h2>Languages</h2>
                                <SkillsGraph data={AboutMe.getLanguageSkillsDataset()} />
                                <h2>Technologies</h2>
                                <SkillsGraph data={AboutMe.getTechSkillsDataset()} />
                            </Card.Body>
                        </Card>
                    </div>
                </div>
            </div>
        );
    }
}
