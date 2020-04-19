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
                value: 6,
            },
            {
                label: 'Python',
                color: '#3cba9f',
                value: 3,
            },
            {
                label: 'JavaScript',
                color: '#e8c3b9',
                value: 3,
            },
        ];
    }

    static getTechSkillsDataset() {
        return [
            {
                label: '.Net Core / ASP.NET',
                color: '#3e95cd',
                value: 6,
            },
            {
                label: 'Django',
                color: '#3cba9f',
                value: 2,
            },
            {
                label: 'React',
                color: '#e8c3b9',
                value: 2,
            },
            {
                label: 'jQuery',
                color: '#8e5ea2',
                value: 2,
            },
            {
                label: 'VueJs',
                color: '#3e95cd',
                value: 1,
            },
        ];
    }

    render() {
        return (
            <div className='page'>
                <div className='aboutMe'>
                    <img className='aboutMeBanner' src='/assets/JoshOutOfFocus.jpg' alt="Josh Harmon" />
                    <h1>
                        <center>Software Engineer + Visual Artist</center>
                    </h1>
                    <p>
                        By profession I am a software engineer working in the payments and banking data space.
                        Throughout my career I've had the opportunity to work on and build a large variety of apps and
                        services ranging from realtime ACH payment processors to interactive webapps. Conversely I've
                        also had the pleasure of learning from many intelligent and insightful coworkers. Much of my
                        career has been in the .NET ecosystem, both deep in legacy .NET Framework but also with modern
                        cross platform .NET Core. However recently I've dived deep into Python and Django development.
                        Regardless of what tools or frameworks I may be working with my guiding principles remain
                        consistent - readability is cleaner than conciseness and simple is better than complex.
                        <br />
                        <br />
                        By hobby I am a film based photographer, amatuer videographer, and overal visual enthusiast.
                        Perhaps to balance the digital nature of my daily life I choose to use film and analog processes
                        for my hobbies whenever I can. More information and examples of my visual work can be found on
                        my media website,{' '}
                        <a href='https://joshharmonimages.com' target='_blank' rel="noopener noreferrer">
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
                                        <Card.Title>Software Engineer</Card.Title>
                                        <Card.Body>
                                            <p>
                                                Lead and implement technical bank feed and financial data integrations
                                                with Financial Partners
                                            </p>
                                            <ListGroup>
                                                <ListGroupItem>VueJS</ListGroupItem>
                                                <ListGroupItem>Python + Django</ListGroupItem>
                                                <ListGroupItem>Postgres SQL</ListGroupItem>
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
                                                Maintain and enhance payment processor infrastructure and back office
                                                tooling
                                            </p>
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
