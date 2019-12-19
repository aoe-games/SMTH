# Welcome!
Here, on GitHub, you can browse development history, file changes, pull requests as well as access a number of other features that help manage the workflow of the project. Please follow the getting started guide below get up and running.

If you have any questions about the project in general, please contact Dave Barton.

## Getting Started

### Development Tools
###### These are some tools and supported versions recommended for working on this project.
**Unity Editor:** 2019.1.1f1 ([download](https://unity3d.com/get-unity/download))  

**Code Editor:** Microsoft Visual Studio Community 2017 ([download](https://visualstudio.microsoft.com/downloads/))  
**Text Editor:** Atom ([download](https://atom.io/))  
**Alternative Code/Text Editor:** Visual Studio Code ([download](https://code.visualstudio.com/Download))  

**Git Visual Tool:** SourceTree ([download](https://www.sourcetreeapp.com/))  
**Git (if using command line):** Git repository management tool ([download](https://git-scm.com/downloads))

## Git Workflow
### General
The SMTH project uses Git to manage source control. You can learn more about Git [here](https://git-scm.com/). Git is used to control and track the flow of changes into the project. This includes changes to various files that contribute to the overall project implementation like source code files, Unity project files, art exports, etc.
  
To clone and access this repository, you can download a visualization tool or use the command line. _SourceTree_ is recommended unless you have another preferred tool or are very comfortable with the command line interface. If you are often pushing changes to the repository, it is advised you [setup an SSH key](https://help.github.com/en/enterprise/2.15/user/articles/adding-a-new-ssh-key-to-your-github-account) so that you can easily push to the repository without having to always provide your access credentials.

### Forking

For this project, it is preferred that all contributors fork the main repository instead of branching and doing work directly in the main repo. This workflow has been adopted to help keep the main repository clean of too much branch "noise". Forks help with this because a fork's branches will not be immediately visible to any viewers of the main repo when browsing revision history and branches.  

**Note:** When making a fork, it is recommended that you name your own fork `origin` and the main repository `upstream` for consistency and clarity between team members.

### Branching

Branching is the mechanism used to complete new work outside of the stable `develop` branch or collerative feature branches. This allows for test assets and incomplete work to be safely pushed to one's own branches without compromisng the stability of other shared branches.

The `develop` branch is the stable, upstream branch that all work can be _safely_ based off of, which is why it is important to branch off of it when doing new work. In addition to this "mainline" branch, upstream feature branches can also be made so that others can easily find and collaborate on a feature outside of the mainline `develop` branch.

The branch naming convention is `ticket-id-short-description`. For example, `PROJ-121-Options-Menu-Integration`. This standardization helps other contributors to find eachother's work and associate it with a particular feauture or fix.  

For general documentation updates not directly related to a ticket, the prefix `DOC-` can be used.

### Integrating Work - Pull Requests

Before work is committed to a shared upstream branch, a pull request (PR) is created against the target branch giving the team an opportunity to review the work and recommend changes before the feature or fix is brought into a main repository branch.  

PRs that are targeting the mainline `develop` branch should be stable and code complete as the rest of the team will be basing their work off of `develop` and release branches. When creating a PR against a shared feature branch, code can be in any state that the collaborators agree on as feature branches are considered a work-in-progress until they are ready to be mainlined. 

#### Opening a Pull Request

You can open a PR by selecting "Pull Requests" tab above and then selecting "New Pull Request". From there, you can select "Compare Across Forks" and select the target fork and branch the changes should be applied to. Then, select the branch from your fork you want merged into the target branch.  

For the title, the preferred format is, for example, `PROJ-121: Option menu integration`. You can then add a detailed description of the work completed, areas effected and any other information that may be useful for the review or testing of the work. You can also add any labels, request reviewers, etc. once the PR is opened.  

#### Closing Pull Requests

When merging a PR, use the _Squash and Merge_ option. This will combine all your commits into a single commit and place it on top of the target branch. Also, please use the following format for commit message titles for when merging PRs through GitHub: `ticket-id: Short description`. For example, `PROJ-121: Option menu integration`. This formatting standard makes it much eas1ier to search and idenify units of work in the project history.

### Submodules
This project uses the following submodules in order to manage code that can be shared between projects. Please be sure to checkout all required submodules or the project may not behave as intended.  

Submodules contain code that is "upstream" or does not otherwise have dependancies on this repository, while this repository has dependancies on its submodules. Be sure that code this is specific to this project is not committed to the submodules as other projects that use the submodule will then get those changes as well! If you are developing something that can be considered a generic tool that could be useful to other projects, consider adding them to an appropriate submodule so those changes can be used by other users.  
