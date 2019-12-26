import React, { Component } from 'react';


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
                    <div class="aboutMeMain">
                    </div>
                </div>
            </div>
        );
    }
}