# duck-utils
A collection of Unity utils

## Features
| Feature       | Description | Docs  |
|:------------- |:------------| :-----|
| MathUtils | A collection of static utils to help with mathematics. | [Docs](Docs/MathUtils.md) |
| Trigonometry | A collection of static utils to help with trigonometry | [Docs](Docs/Trigonometry.md) |
| EventBroadcaster | A mechanism for sending events/messages to components on game objects. | [Docs](Docs/EventBroadcaster.md) |
| Timer | A callback based, self updating timer. | [Docs](Docs/Timer.md) |
| MonoBehaviourService | Service that allows subcription to mono behaviour messages as events | [Docs](Docs/MonoBehaviourService.md) |
| TransformSnapshot | A util for taking snapshots of transform properties and applying them | [Docs](Docs/TransformSnapshot.md) |

## Releasing
* Use [gitflow](https://nvie.com/posts/a-successful-git-branching-model/)
* Create a release branch for the release
* On that branch, bump version number in package json file, any other business (docs/readme updates)
* Merge to master via pull request and tag the merge commit on master.
* Merge back to development.

## DUCK

This repo is part of DUCK (dubit unity component kit)
DUCK is a series of repos containing reusable component, utils, systems & tools. 

DUCK packages can be added to a project as git submodules or by using [Unity Package Manager](https://docs.unity3d.com/Manual/upm-git.html). 
