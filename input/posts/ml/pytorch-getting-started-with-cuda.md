Title: Getting Started with PyTorch and NVidia CUDA
Lead: Installing PyTorch with NVidia CUDA support
Published: 09/30/2022
Tags:
  - Machine Learning
  - pytorch
  - cuda
---
### Installing PyTorch
Official [pytorch.org install instructions](https://pytorch.org/get-started/locally) offers stable version version of pyTorch with slightly older version of CUDA which is 11.6 instead of latest (11.7)

We wanna show here how to install it with CUDA 11.7 SDK Toolkit support.

Start a shell (powershell/command prompt with python added to path),

    pip3 install --pre torch torchvision torchaudio --extra-index-url https://download.pytorch.org/whl/nightly/cu117 --upgrade


**refs**
- Official install instructions: https://pytorch.org/get-started/locally
- CUDA 11.7 on pytroch official site doesn't exist yet, https://download.pytorch.org/whl/cu117
- CloudCasts - Alan Smith - PyTorch & CUDA Setup - Windows 10 https://www.youtube.com/watch?v=GMSjDTU8Zlc

- Platform nvidia refs
 - [Linux x64 build](https://developer.nvidia.com/cuda-downloads?target_os=Linux&target_arch=x86_64)
 - [Windows x64 build](https://developer.nvidia.com/cuda-downloads?target_os=Windows&target_arch=x86_64&target_version=11)
 - https://developer.nvidia.com/cuda-toolkit

