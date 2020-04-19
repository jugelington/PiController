# PiController
PiController is a simple command-line application created to make remote management of a Raspberry Pi server a little easier. The initial implementation allows you to start/stop/restart systemd services without having to manually type out the required system commands yourself. 

## Getting Started
All of these steps assume you already have a server up and running.

Due to the nature of the permissions being given to the Pi user in this tutorial, it is highly recommended that you create a unique user for this program to use. 
 
1) Create an `appsettings.json` file, in the same folder as this readme, with a bit of basic config, in the following format:
```json
{
    "SshSettings": {
        "Host": "address of your server",
        "Username": "your user on the server"
    },
    "SystemServices": [
        "first-service",
        "second-service"
    ]
}
```
2) Configure your Pi to be accessible via an SSH key. A guide on how to do this can be found [here](https://www.raspberrypi.org/documentation/remote-access/ssh/passwordless.md#copy-your-public-key-to-your-raspberry-pi).

3) Copy the SSH key created above to `Settings\id_rsa`

4) Optionally, you can disable password authentication for accessing your Pi remotely. You can do that by adding the line
 `PasswordAuthentication no` to `/etc/ssh/ssh_config`.

5) Setup your user to not need a password to restart system services, by creating a file in `/etc/sudoers/sudoers.d/` with the following content for each service you wish to manage. Note that I am unhappy with this solution, and will be looking int a more elegant one.
    
```
your-user-name ALL=NOPASSWD: /bin/systemctl restart your-service.service, /bin/systemctl stop your-service.service, /bin/systemctl start your-service.service
```