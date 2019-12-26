import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import './about-me.css';


export class AboutMe extends Component {
    static displayName = AboutMe.name;

    constructor(props) {
        super(props);
    }

    static buildTabs() {
        return (
            <div>
                <Tabs
                    className="tabs"
                    selectedTabClassName="tabSelected"
                    selectedTabPanelClassName="panelSelected"
                >
                    <TabList className="tabList">
                        <Tab className="tab">Professional</Tab>
                        <Tab className="tab">Personal</Tab>
                    </TabList>
                    <TabPanel className="tabPanel">
                        {/* Professional */}
                        <h3>Professional</h3>
                        <p>
                            Lorem dfsgsd dfg d dfg dfg dipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper. Suspendisse mattis sapien nec eros ornare euismod at quis massa. Aenean sit amet vulputate dolor. Praesent placerat hendrerit fringilla. Pellentesque aliquet lobortis porta. Fusce euismod rutrum orci tempor blandit. Vivamus gravida lobortis justo, non suscipit nulla gravida sit amet. Nunc sodales risus non consequat congue. Nulla nulla ipsum, mattis eu mi vitae, semper vestibulum velit. Aenean condimentum purus sed neque cursus semper. Maecenas blandit in tortor efficitur mattis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris ullamcorper id urna sed tempus.
                        </p>
                    </TabPanel>
                    <TabPanel className="tabPanel">
                        {/* Personal */}
                        <h3>Personal</h3>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper. Suspendisse mattis sapien nec eros ornare euismod at quis massa. Aenean sit amet vulputate dolor. Praesent placerat hendrerit fringilla. Pellentesque aliquet lobortis porta. Fusce euismod rutrum orci tempor blandit. Vivamus gravida lobortis justo, non suscipit nulla gravida sit amet. Nunc sodales risus non consequat congue. Nulla nulla ipsum, mattis eu mi vitae, semper vestibulum velit. Aenean condimentum purus sed neque cursus semper. Maecenas blandit in tortor efficitur mattis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris ullamcorper id urna sed tempus.
                        </p>
                    </TabPanel>
                </Tabs>
            </div>
        );
    }

    render() {
        return (
            <div class="page">
                <div class="aboutMe">
                    <img src="/assets/JoshOutOfFocus.jpg" />
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sodales at elit a ullamcorper. Suspendisse mattis sapien nec eros ornare euismod at quis massa. Aenean sit amet vulputate dolor. Praesent placerat hendrerit fringilla. Pellentesque aliquet lobortis porta. Fusce euismod rutrum orci tempor blandit. Vivamus gravida lobortis justo, non suscipit nulla gravida sit amet. Nunc sodales risus non consequat congue. Nulla nulla ipsum, mattis eu mi vitae, semper vestibulum velit. Aenean condimentum purus sed neque cursus semper. Maecenas blandit in tortor efficitur mattis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris ullamcorper id urna sed tempus.
                    </p>
                    {AboutMe.buildTabs()}
                </div>
            </div>
        );
    }
}